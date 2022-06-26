using Data_Access_Layer.Entity;
using Data_Access_Layer.Repository;
using Microsoft.AspNetCore.Mvc;
using Presentation_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentation_Layer.Controllers
{
    public class ReimbursementController : Controller
    {
        public readonly IReimbursementRepository iReimbursementRepository =null;

        /// <summary>
        /// Constructor method for initialization of IReimbursementRepository interface's instance using ReimbursementRepository instance
        /// </summary>
        /// <param name="reimbursementRepository"></param>
        public ReimbursementController(ReimbursementRepository reimbursementRepository)
        {
            iReimbursementRepository = reimbursementRepository;
        }

        /// <summary>
        /// Call the AddReimbursement method to Add new Reimbursement
        /// </summary>
        /// <param name="reimbursement">Details for Reimbursement to be added</param>
        /// <returns>Returns true if successfully added, else return false</returns>
        [HttpPost]
        [Route("AddReimbursement")]
        public async Task<bool> AddReimbursement([FromBody] ReimbursementEntity reimbursement)
        {
            if (await iReimbursementRepository.AddReimbursement(reimbursement))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Call GetAllReimbursement method to find all reimbursements
        /// </summary>
        /// <returns>All reimbursements in JSON format</returns>
        [HttpGet]
        [Route("GetReimbursement")]
        public async Task<string> GetAllReimbursements()
        {
            var Result = await iReimbursementRepository.GetAllReimbursements();
            var Json = JsonSerializer.Serialize(Result);

            return Json;
        }

        /// <summary>
        /// Call GetDeclinedReimbursements method to find all Declined reimbursements
        /// </summary>
        /// <returns>Declined reimbursements in JSON format</returns>
        [HttpGet]
        [Route("GetPendingReimbursements")]
        public async Task<string> GetPendingReimbursements()
        {
            var Result = await iReimbursementRepository.GetPendingReimbursements();
            var Json = JsonSerializer.Serialize(Result);

            return Json;
        }

        /// <summary>
        /// Call GetDeclinedReimbursements method to find all Declined reimbursements
        /// </summary>
        /// <returns>Declined reimbursements in JSON format</returns>
        [HttpGet]
        [Route("GetDeclinedReimbursements")]
        public async Task<string> GetDeclinedReimbursements()
        {
            var Result = await iReimbursementRepository.GetDeclinedReimbursements();
            var Json = JsonSerializer.Serialize(Result);

            return Json;
        }

        /// <summary>
        /// Call GetApprovedReimbursements method to find all Approved reimbursements
        /// </summary>
        /// <returns>Approved reimbursements in JSON format</returns>
        [HttpGet]
        [Route("GetApprovedReimbursements")]
        public async Task<string> GetApprovedReimbursements()
        {
            var Result = await iReimbursementRepository.GetApprovedReimbursements();
            var Json = JsonSerializer.Serialize(Result);

            return Json;
        }

        /// <summary>
        /// Call GetReimbursementByEmployeeId method to find reimbursements of specific employee
        /// </summary>
        /// <param name="id">Unique EmployeeID</param>
        /// <returns>Reimbursements in JSON format</returns>
        [HttpGet]
        [Route("GetReimbursementById/{id}")]
        public async Task<string> GetReimbursementById(int id)
        {
            var Result = await iReimbursementRepository.GetReimbursementByEmployeeId(id);
            var Json = JsonSerializer.Serialize(Result);

            return Json;
        }

        /// <summary>
        /// Calls ApproveReimbursement method to update reimbursement record as Approved
        /// </summary>
        /// <param name="reimbursementDetails">Reimbursement to be Approved</param>
        /// <returns>Details of Approved Reimbursement</returns>
        [HttpPut]
        [Route("ApproveReimbursement")]
        public async Task<RequestPhaseEntity> ApproveReimbursement([FromBody] RequestPhaseEntity reimbursementDetails)
        {
            var Details = new RequestPhaseEntity()
            {
                ReimbursementID = reimbursementDetails.ReimbursementID,
                ApprovedValue = reimbursementDetails.ApprovedValue,
                ApprovedBy = reimbursementDetails.ApprovedBy,
                InternalNotes = reimbursementDetails.InternalNotes
            };
            await iReimbursementRepository.ApproveReimbursement(Details);
            return Details;
        }

        /// <summary>
        /// Calls DeclineReimbursement method to update reimbursement record as Declined
        /// </summary>
        /// <param name="reimbursementId"></param>
        /// <returns>True if Declined else false</returns>
        [HttpPut]
        [Route("DeclineReimbursement")]
        public async Task<bool> DeclineReimbursement([FromBody] int reimbursementId)
        {
            if (await iReimbursementRepository.DeclineReimbursement(reimbursementId))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Call UpdateReimbursement method to Update a reimbursement's details
        /// </summary>
        /// <param name="reimbursementDetails"></param>
        /// <returns>Updated Values of reimbursement</returns>
        [HttpPut]
        [Route("UpdateReimbursement")]
        public async Task<UpdateEntity> UpdateReimbursement([FromBody] UpdateEntity reimbursementDetails)
        {
            var Details = new UpdateEntity()
            {
                Date = reimbursementDetails.Date,
                ReimbursementID=reimbursementDetails.ReimbursementID,
                ReimbursementType = reimbursementDetails.ReimbursementType,
                RequestedValue = reimbursementDetails.RequestedValue,
                Currency = reimbursementDetails.Currency,
                ImageURL = reimbursementDetails.ImageURL
            };
            await iReimbursementRepository.UpdateReimbursement(Details);
            return Details;
        }

        /// <summary>
        /// Call DeleteReimbursement method to remove Reimbursement
        /// </summary>
        /// <param name="reimbursementID"></param>
        /// <returns>True if Reimbursement removed from database else return false</returns>
        [HttpDelete]
        [Route("DeleteReimbursement/{id}")]
        public async Task<ActionResult<bool>> DeleteReimbursement(int reimbursementID)
        {
            var Check = await iReimbursementRepository.DeleteReimbursement(reimbursementID);
            if(Check == false)
            {
                return NoContent();
            }
            return true;
        }

        /// <summary>
        /// Call GetMonthsCount method to get Month-Wise count of reimbursements
        /// </summary>
        /// <returns>Integer Array(Lenght 12) of Reimbursement count</returns>
        [HttpGet]
        [Route("GetMonthsCount")]
        public int[] GetMonthsCount()
        {
            var Result = iReimbursementRepository.GetMonthsCount();
            return Result;
        }

        /// <summary>
        /// Call GetCategoryCount method to get Month-Wise count of reimbursements
        /// </summary>
        /// <returns>Integer Array(Lenght 5) of Reimbursement count</returns>
        [HttpGet]
        [Route("GetCategoryCount")]
        public int[] GetCategoryCount()
        {
            var Result = iReimbursementRepository.GetCategoryCount();
            return Result;
        }
    }
}
