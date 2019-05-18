using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGV_Eindproject_WebDevelopment.Domain.Entities;
using TGV_Eindproject_WebDevelopment.Repository;

namespace TGV_Eindproject_WebDevelopment.Service
{
    public class TicketService
    {

        #region Repositories

        private readonly TicketDAO ticketDAO;

        #endregion

        #region Services

        private readonly TGVService tgvService;
        private readonly UserService userService;
        
        #endregion

        #region Constructors

        public TicketService()
        {
            ticketDAO = new TicketDAO();
            tgvService = new TGVService();
            userService = new UserService();
        }

        #endregion

        #region Get-Methods

        public IEnumerable<Tickets> All()
        {
            return ticketDAO.All();
        }

        public IEnumerable<Tickets> AllFromUser(int userId)
        {
            return ticketDAO.AllFromUser(userId);
        }

        public Tickets Get(int id)
        {
            return ticketDAO.Get(id);
        }

        public IEnumerable<Tickets> AllForTGV(int tgvId, DateTime dateOfDeparture)
        {
            if (dateOfDeparture == null)
                throw new ArgumentException("There was no date given.");
            return ticketDAO.AllForTGV(tgvId, dateOfDeparture);
        }

        #endregion

        #region Update-Methods

        public void Cancel(int id)
        {
            Tickets ticket = Get(id);

            if (ticket.DateOfDeparture.Date.AddDays(-3).CompareTo(DateTime.Now.Date) < 0)
                throw new ArgumentException("It is too late to cancel this ticket");

            ticket.IsCancelled = 1;
            ticket.SeatNumber = null;

            ticketDAO.Update(ticket);
        }

        #endregion

        #region Create-Methods

        public void Create(Tickets ticket)
        {
            // replace with call from UserService
            if (userService.Get(ticket.UserId) == null)
                throw new ArgumentException("This user does not exist.");
            if (tgvService.Get(ticket.Tgvid) == null)
                throw new ArgumentException("This TGV does not exist.");
            if (ticket.Name == null)
                throw new ArgumentException("No name given.");
            if (ticket.DateOfDeparture == null)
                throw new ArgumentException("No date given.");
            if (ticket.DateOfDeparture.CompareTo(DateTime.Now) < 0)
                throw new ArgumentException("You cannot create a ticket for a moment before now.");
            if (ticket.IsBusiness != 0 && ticket.IsBusiness != 1)
                throw new ArgumentException("Invalid value for business or economic class.");

            SetSeatNumber(ticket);
            ticket.IsCancelled = 0;
            if (All().Count() > 0)
                ticket.Id = All().Last().Id + 1;

            ticketDAO.Create(ticket);
        }

        #endregion

        #region Helper-Methods

        private void SetSeatNumber(Tickets ticket)
        {
            IList<Tickets> tickets = new List<Tickets>();

            foreach (Tickets t in ticketDAO.AllForTGV(ticket.Tgvid, ticket.DateOfDeparture))
            {
                if (t.IsCancelled == 0 && t.IsBusiness == ticket.IsBusiness)
                    tickets.Add(t);
            }

            if (tickets.Count() >= tgvService.GetAmountOfSeats(ticket.Tgvid, ticket.IsBusiness))
                throw new ArgumentException("This TGV has no seats left for the chosen class.");

            if (ticket.IsBusiness == 0)
                ticket.SeatNumber = "E-" + tickets.Count().ToString();
            else
                ticket.SeatNumber = "B-" + tickets.Count().ToString();
        }

        #endregion

    }
}
