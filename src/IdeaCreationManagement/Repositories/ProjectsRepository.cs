using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using IdeaCreationManagement.Models;
using File = IdeaCreationManagement.Models.File;

namespace IdeaCreationManagement.Repositories
{
    public class ProjectsRepository
    {
        private readonly AppContext _db = new AppContext();

        public void SaveChanges() => _db.SaveChanges();

        public IQueryable<Category> GetProjectsCategories(ProjectType projectType)
        {
            IQueryable<Category> categories = _db.Categories.Where(c => c.Type == projectType);
            return categories;
        }

        public int GetProjectStateId(string stateName)
        {
            var state = _db.States.FirstOrDefault(s => s.Name == stateName);
            if (state != null)
                return state.Id;
            return -1;
        }

        public void AddNewProject(Project project)
        {
            project.AssigneeId = null;
            project.AverageDifficulty = 0;
            project.AverageGrade = 0;
            project.AverageIngenuity = 0;
            project.AverageUsefulness = 0;
            project.Time = DateTime.Now;
            project.StateId = GetProjectStateId("oczekujący");

            _db.Projects.Add(project);
        }

        public void AddFilesIntoProject(Project project, IEnumerable<HttpPostedFileBase> postedFiles)
        {
            foreach (var postedFile in postedFiles)
            {
                File file = new File
                {
                    Name = postedFile.FileName,
                    DataType = postedFile.ContentType,
                    Size = postedFile.ContentLength,
                    ProjectId = project.Id,
                };
                using (var reader = new BinaryReader(postedFile.InputStream))
                {
                    file.Content = reader.ReadBytes(postedFile.ContentLength);
                }
                _db.Files.Add(file);
            }
        }
    }
}