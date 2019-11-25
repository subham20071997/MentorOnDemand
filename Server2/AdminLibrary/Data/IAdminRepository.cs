using AdminLibrary.DTO;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdminLibrary.Data
{
    public interface IAdminRepository
    {
        public Task<bool> AddTechAsync(TechnologyDTO technologyDTO);
        public IEnumerable<Technology> GetTechnologies();
        public Task<Technology> GetTechById(int id);
        public Task<bool> UpdateTechAsync(Technology technology);
        public Task<bool> DeleteTechAsync(Technology technology);
        public Task<IEnumerable<UpdateUserDTO>> GetMentors();
        public Task<IEnumerable<UpdateStudentDTO>> GetStudents();
        public IEnumerable<Technology> GetCourses();
        public Task<CountDTO> GetCount();
        public Task<bool> UpdateUserStatus(UpdateUserStatusDTO updateUserStatus);
    }
}
