using Data_Access_Layer.Data;
using Data_Access_Layer.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository
{
    public class ReimbursementRepository : IReimbursementRepository
    {
        public ReimbursementContext _dbContext;

        public string Declined = "Declined";
        public string Approved = "Approved";

        /// <summary>
        /// Constructor method to initialize the context
        /// </summary>
        /// <param name="context"></param>
        public ReimbursementRepository(ReimbursementContext context)
        {
            _dbContext = context;
        }

        /// <summary>
        /// Adding new Reimbursement requested by some employee
        /// </summary>
        /// <param name="reimbursementDetails">Contains details of reimbursement to be added</param>
        /// <returns>Returns true after adding reimbursement</returns>
        public async Task<bool> AddReimbursement(ReimbursementEntity reimbursementDetails)
        {
            var NewReimbursement = new ReimbursementEntity()
            {
                Date = reimbursementDetails.Date,
                EmployeeID=reimbursementDetails.EmployeeID,
                ReimbursementType = reimbursementDetails.ReimbursementType,
                RequestedValue = reimbursementDetails.RequestedValue,
                ApprovedValue = reimbursementDetails.ApprovedValue,
                Currency = reimbursementDetails.Currency,
                RequestPhase = reimbursementDetails.RequestPhase,
                ImageURL = reimbursementDetails.ImageURL,
                RequestedBy = reimbursementDetails.RequestedBy,
                ApprovedBy = reimbursementDetails.ApprovedBy,
                InternalNotes = reimbursementDetails.InternalNotes

            };
            _dbContext.Reimbursement.Add(NewReimbursement);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Update exisiting reimbursement
        /// </summary>
        /// <param name="reimbursementDetails">Reimbursement to be updated with updated record</param>
        /// <returns>Returns true if reimbursement is updated else false</returns>
        public async Task<bool> UpdateReimbursement(UpdateEntity reimbursementDetails)
        {
            if (reimbursementDetails is null)
            {                 
                return false;
            }
            else
            {                
                var RecordToBeUpdated = _dbContext.Reimbursement.Where(d => d.ReimbursementID == reimbursementDetails.ReimbursementID).FirstOrDefault();
                RecordToBeUpdated.Date = reimbursementDetails.Date;
                RecordToBeUpdated.ReimbursementID = reimbursementDetails.ReimbursementID;
                RecordToBeUpdated.ReimbursementType = reimbursementDetails.ReimbursementType;
                RecordToBeUpdated.RequestedValue = reimbursementDetails.RequestedValue;
                RecordToBeUpdated.Currency = reimbursementDetails.Currency;
                RecordToBeUpdated.ImageURL = reimbursementDetails.ImageURL;

                await _dbContext.SaveChangesAsync();
                return true;
            }
            
        }

        /// <summary>
        /// Find and Delete Reimbursement using its unique Reimbursement ID
        /// </summary>
        /// <param name="reimbursementID">Contains the Reimbursement ID to be deleted</param>
        /// <returns>Returns True if Reimbursement is updated, else return false</returns>
        public async Task<bool> DeleteReimbursement(int reimbursementID)
        {
            var Details = await _dbContext.Reimbursement.FindAsync(reimbursementID);
            if(Details!=null)
            {
                _dbContext.Reimbursement.Remove(Details);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Getting all reimbursements ever been added or updated
        /// </summary>
        /// <returns>Returns List of all Reimbursements</returns>
        public async Task<List<ReimbursementEntity>> GetAllReimbursements()
        {
            var reimbursement = new List<ReimbursementEntity>();
            var Result = await _dbContext.Reimbursement.ToListAsync();
            if (Result?.Any() == true)
            {
                foreach (var e in Result)
                {
                    reimbursement.Add(new ReimbursementEntity()
                    {
                        EmployeeID = e.EmployeeID,
                        ReimbursementID = e.ReimbursementID,
                        Date = e.Date,
                        ReimbursementType = e.ReimbursementType,
                        RequestedBy=e.RequestedBy,
                        RequestedValue = e.RequestedValue,
                        ApprovedValue = e.ApprovedValue,
                        Currency = e.Currency,
                        RequestPhase = e.RequestPhase,
                        ImageURL = e.ImageURL
                    });
                }
                return reimbursement;
            }
            return Result;
        }


        /// <summary>
        /// Getting the list of Reimbursements requested by an employee using his unique EmployeeID
        /// </summary>
        /// <param name="employeeID">Unique ID of every Employee</param>
        /// <returns>List of Reimbursements added by an employee</returns>
        public async Task<List<ReimbursementEntity>> GetReimbursementByEmployeeId(int? employeeID)
        {
            var Result = await _dbContext.Reimbursement.Where(x => x.EmployeeID == employeeID).ToListAsync();
            return Result;
        }


        /// <summary>
        /// Approve a Reimbursement
        /// </summary>
        /// <param name="reimbursementDetails">Reimbursement to be updated</param>
        /// <returns>Returns True if Approved, else returns false</returns>
        public async Task<bool> ApproveReimbursement(RequestPhaseEntity reimbursementDetails)
        {
            if (reimbursementDetails is null)
            {
                return false;
            }
            else
            {
                var RecordToBeUpdated = _dbContext.Reimbursement.Where(d => d.ReimbursementID == reimbursementDetails.ReimbursementID).FirstOrDefault();
                RecordToBeUpdated.ReimbursementID = reimbursementDetails.ReimbursementID;
                RecordToBeUpdated.ApprovedValue = reimbursementDetails.ApprovedValue;
                RecordToBeUpdated.ApprovedBy = reimbursementDetails.ApprovedBy;
                RecordToBeUpdated.InternalNotes = reimbursementDetails.InternalNotes;
                RecordToBeUpdated.RequestPhase = "Approved";

                await _dbContext.SaveChangesAsync();
                return true;
            }
        }

        /// <summary>
        /// Decline a Reimbursement
        /// </summary>
        /// <param name="reimbursementId">Unique ID of the Reimbursement to be Declined</param>
        /// <returns>Returns True if RequestPhase setted to Declined, else returns false</returns>
        public async Task<bool> DeclineReimbursement(int? reimbursementId)
        {
            if (reimbursementId is null)
            {
                return false;
            }
            else
            {
                var RecordToBeUpdated = _dbContext.Reimbursement.Where(d => d.ReimbursementID == reimbursementId).FirstOrDefault();
                RecordToBeUpdated.RequestPhase = "Declined";
                RecordToBeUpdated.ApprovedValue = 0;
                await _dbContext.SaveChangesAsync();
                return true;
            }
        }

        /// <summary> 
        /// </summary>
        /// <returns>Returns all the reimbursements whose RequestPhase=null</returns>
        public async Task<List<ReimbursementEntity>> GetPendingReimbursements()
        {
            var Result = await _dbContext.Reimbursement.Where(x => x.RequestPhase == null).ToListAsync();
            return Result;
        }

        /// <summary> 
        /// </summary>
        /// <returns>Returns all the reimbursements whose RequestPhase="Approved"</returns>
        public async Task<List<ReimbursementEntity>> GetDeclinedReimbursements()
        {
            var Result = await _dbContext.Reimbursement.Where(x => x.RequestPhase == Declined).ToListAsync();
            return Result;
        }

        /// <summary> 
        /// </summary>
        /// <returns>Returns all the reimbursements whose RequestPhase="Declined"</returns>
        public async Task<List<ReimbursementEntity>> GetApprovedReimbursements()
        {
            var Result = await _dbContext.Reimbursement.Where(x => x.RequestPhase == Approved).ToListAsync();
            return Result;
        }

        /// <summary>
        /// Get count of reimbursements Month-Wise
        /// </summary>
        /// <returns>Integer Array(Lenght 12) of Reimbursement count</returns>
        public int[] GetMonthsCount()
        {
            var Reimbursements = new List<ReimbursementEntity>();
            var Result = _dbContext.Reimbursement.ToList();
            if (Result?.Any() == true)
            {
                foreach (var e in Result)
                {
                    Reimbursements.Add(new ReimbursementEntity()
                    {
                        Date = e.Date
                    });
                }
            }
            int[] MonthCount = new int[13];
            DateTime Date;
            int MonthNumber,number;
            int[] Counts = new int[12];
            /// <summary>
            /// Increase the number of count for reimbursement at its equivalent Month at respected index.
            /// Index zero count will always be zero because there is no respective month number
            /// </summary>
            foreach (var e in Reimbursements)
            {
                Date = e.Date;
                MonthNumber = Date.Month;
                number = MonthNumber;
                ++MonthCount[number];
            }
            /// <summary>
            /// Shift all months count to an array of length 12
            /// </summary>
            for(int Loop=0;Loop< Counts.Length;Loop++)
            {
                Counts[Loop] = MonthCount[Loop + 1];
            }
            return Counts;
        }

        /// <summary>
        /// Get count of reimbursements Category-Wise
        /// </summary>
        /// <returns>Integer Array(Lenght 5) of Reimbursement count</returns>
        public int[] GetCategoryCount()
        {
            int[] CategoryCount = new int[5];
            var Reimbursements = _dbContext.Reimbursement.ToList();
            if (Reimbursements?.Any() == true)
            {
                foreach (var LoopItem in Reimbursements)
                {
                    if (LoopItem.ReimbursementType == "Medical")
                        CategoryCount[0]++;
                    else if (LoopItem.ReimbursementType == "Food")
                        CategoryCount[1]++;
                    else if (LoopItem.ReimbursementType == "Travel")
                        CategoryCount[2]++;
                    else if (LoopItem.ReimbursementType == "Entertainment")
                        CategoryCount[3]++;
                    else if (LoopItem.ReimbursementType == "Misc.")
                        CategoryCount[4]++;
                }
                return CategoryCount;
            }
            else
                return null;
         
        }
    }
}
