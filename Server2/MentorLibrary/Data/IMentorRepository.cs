using MentorLibrary.DTO;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MentorLibrary.Data
{
    public interface IMentorRepository
    {
        public Task<IEnumerable<MentorSkillGetDTO>> GetSkills(string email);
        public MentorSkillGetDTO GetSkill(string email, int techId);
        public Task<int> AddSkill(MentorSkillAddDTO mentorSkillAddDTO);
        public Task<bool> UpdateSkill(MentorSkillAddDTO updateSkillDTO);
        public IEnumerable<Technology> GetTechnologies();
        public bool DeleteSkill(int id, string email);
    }
}
