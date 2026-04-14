using Microsoft.AspNetCore.Mvc;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll([FromQuery] DateOnly? date, [FromQuery] string? status, [FromQuery] int? roomId)
    {
        var reservations = AppData.Reservations.AsEnumerable();

        if (date.HasValue)
            reservations = reservations.Where(r => r.Date == date.Value);

        if (!string.IsNullOrWhiteSpace(status))
            reservations = reservations.Where(r => r.Status.ToLower() == status.ToLower());

        if (roomId.HasValue)
            reservations = reservations.Where(r => r.RoomId == roomId.Value);

        return Ok(reservations);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var reservation = AppData.Reservations.FirstOrDefault(r => r.Id == id);

        if (reservation is null)
            return NotFound();

        return Ok(reservation);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Reservation reservation)
    {
        var room = AppData.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);

        if (room is null)
            return BadRequest("Sala o podanym id nie istnieje");

        if (!room.IsActive)
            return Conflict("Nie można dodać rezerwacji do nieaktywnej sali");

        bool hasConflict = AppData.Reservations.Any(r =>
            r.RoomId == reservation.RoomId &&
            r.Date == reservation.Date &&
            r.Status.ToLower() != "cancelled" &&
            reservation.Status.ToLower() != "cancelled" &&
            reservation.StartTime < r.EndTime &&
            reservation.EndTime > r.StartTime);

        if (hasConflict)
            return Conflict("Rezerwacja koliduje czasowo z istniejącą rezerwacją tej samej sali");

        int newId = AppData.Reservations.Any() ? AppData.Reservations.Max(r => r.Id) + 1 : 1;

        reservation.Id = newId;
        AppData.Reservations.Add(reservation);

        return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] Reservation updatedReservation)
    {
        var reservation = AppData.Reservations.FirstOrDefault(r => r.Id == id);

        if (reservation is null)
            return NotFound();

        var room = AppData.Rooms.FirstOrDefault(r => r.Id == updatedReservation.RoomId);
        if (room is null)
            return BadRequest("Sala o podanym id nie istnieje");

        if (!room.IsActive)
            return Conflict("Nie można przypisać rezerwacji do nieaktywnej sali");

        bool hasConflict = AppData.Reservations.Any(r =>
            r.Id != id &&
            r.RoomId == updatedReservation.RoomId &&
            r.Date == updatedReservation.Date &&
            r.Status.ToLower() != "cancelled" &&
            updatedReservation.Status.ToLower() != "cancelled" &&
            updatedReservation.StartTime < r.EndTime &&
            updatedReservation.EndTime > r.StartTime);

        if (hasConflict)
            return Conflict("Zaktualizowana rezerwacja koliduje czasowo z inną rezerwacją");

        reservation.RoomId = updatedReservation.RoomId;
        reservation.OrganizerName = updatedReservation.OrganizerName;
        reservation.Topic = updatedReservation.Topic;
        reservation.Date = updatedReservation.Date;
        reservation.StartTime = updatedReservation.StartTime;
        reservation.EndTime = updatedReservation.EndTime;
        reservation.Status = updatedReservation.Status;

        return Ok(reservation);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var reservation = AppData.Reservations.FirstOrDefault(r => r.Id == id);

        if (reservation is null)
            return NotFound();

        AppData.Reservations.Remove(reservation);
        return NoContent();
    }
}