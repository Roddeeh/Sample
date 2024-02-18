using MediatR;
using Sample.Application.Models;

namespace Sample.Application.Commands
{
    public class EstabilishTickerConnecionCommand: IRequest<Unit>
    {
        public DateTime InitialDate { get; set; }
        public int Count { get; set; }
        public double Volume { get; set; }

        public EstabilishTickerConnecionCommand(DateTime initialDate, int count, double volume)
        {
            InitialDate = initialDate;
            Count = count;  
            Volume = volume;
        }
    }
}
