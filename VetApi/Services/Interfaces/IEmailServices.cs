using System.Threading.Tasks;
using FluentResults;
using VetApi.Data;
using VetApi.Models;

namespace VetApi.Services.Interfaces
{
    public interface IEmailServices
    {
        Task<Result> ConfirmAccount(ConfirmEmailRequest request);
        void SendEmail(Message msg);
        
    }
}