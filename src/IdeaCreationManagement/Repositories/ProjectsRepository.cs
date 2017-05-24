using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdeaCreationManagement.Models;

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
    }
}