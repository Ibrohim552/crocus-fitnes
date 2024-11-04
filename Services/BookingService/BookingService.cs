using AutoMapper;
using CrocusFitnes;
using CrocusFitnes.Dto_s;
using CrocusFitnes.Entities;
using CrocusFitnes.Responces;
using CrocusFitnes.Result;
using GymSphere.Filters;
using Microsoft.EntityFrameworkCore;

namespace GymSphere.Services.BookingService;

public sealed class BookingService : IBookingService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public BookingService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PagedResponse<IEnumerable<BookingReadInfo>>>> GetBookings(BookingFilter filter)
    {
        IQueryable<Booking> bookings = _context.Bookings;

        int count = await bookings.CountAsync();

        var pagedBookings = bookings
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize);

        var bookingDtos = _mapper.ProjectTo<BookingReadInfo>(pagedBookings);

        var response = PagedResponse<IEnumerable<BookingReadInfo>>.Create(filter.PageNumber, filter.PageSize, count, bookingDtos);

        return Result<PagedResponse<IEnumerable<BookingReadInfo>>>.Success(response);
    }

    public async Task<Result<BookingReadInfo>> GetBookingById(int id)
    {
        Booking? result = await _context.Bookings.FirstOrDefaultAsync(x => x.Id == id);

        return result is null
            ? Result<BookingReadInfo>.Failure(Error.NotFound())
            : Result<BookingReadInfo>.Success(_mapper.Map<BookingReadInfo>(result));
    }

    public async Task<BaseResult> CreateBooking(BookingCreateInfo info)
    {
        if (info == null)
            return BaseResult.Failure(Error.NotFound());

        var booking = _mapper.Map<Booking>(info);
        await _context.Bookings.AddAsync(booking);
        int res = await _context.SaveChangesAsync();

        return res == 0
            ? BaseResult.Failure(Error.InternalServerError("Data not saved"))
            : BaseResult.Success();
    }

    public async Task<BaseResult> UpdateBooking(int id, BookingUpdateInfo info)
    {
        Booking? existingBooking = await _context.Bookings.AsTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (existingBooking is null)
            return BaseResult.Failure(Error.NotFound());

        _mapper.Map(info, existingBooking);
        int res = await _context.SaveChangesAsync();

        return res == 0
            ? BaseResult.Failure(Error.InternalServerError("Data not updated"))
            : BaseResult.Success();
    }

    public async Task<BaseResult> DeleteBooking(int id)
    {
        Booking? existingBooking = await _context.Bookings.AsTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (existingBooking is null)
            return BaseResult.Failure(Error.NotFound());

        _context.Bookings.Remove(existingBooking);
        int res = await _context.SaveChangesAsync();

        return res == 0
            ? BaseResult.Failure(Error.InternalServerError("Data not deleted"))
            : BaseResult.Success();
    }
}
