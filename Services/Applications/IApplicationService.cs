﻿using Lagalt_Backend.Models.Domain;

namespace Lagalt_Backend.Services.ApplicationServices
{
    public interface IApplicationService : ICrudService<Application, int>
    {
        /// <summary>
        /// Check if the application exists in the database by ID.
        /// </summary>
        /// <param name="id">Application ID</param>
        /// <returns>True if the application exists, false if not.</returns>
        public Task<bool> ApplicationExists(int id);
        public Task UpdateApprovalStatus(int id);
    }
}
