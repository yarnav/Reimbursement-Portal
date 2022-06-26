using Data_Access_Layer.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository
{
    public interface IReimbursementRepository
    {
        Task<bool> AddReimbursement(ReimbursementEntity reimbursementDetails);
        Task<List<ReimbursementEntity>> GetAllReimbursements();
        Task<List<ReimbursementEntity>> GetReimbursementByEmployeeId(int? id);
        Task<bool> UpdateReimbursement(UpdateEntity reimbursementDetails);
        Task<bool> DeleteReimbursement(int id);
        Task<bool> DeclineReimbursement(int? reimbursementId);
        Task<bool> ApproveReimbursement(RequestPhaseEntity reimbursementDetails);
        Task<List<ReimbursementEntity>> GetPendingReimbursements();
        Task<List<ReimbursementEntity>> GetDeclinedReimbursements();
        Task<List<ReimbursementEntity>> GetApprovedReimbursements();
        int[] GetMonthsCount();
        int[] GetCategoryCount();
    }
}