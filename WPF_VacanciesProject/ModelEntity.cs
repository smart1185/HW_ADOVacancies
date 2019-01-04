namespace WPF_VacanciesProject
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelEntity : DbContext
    {
        public ModelEntity()
            : base("name=ModelEntity")
        {
        }

        public virtual DbSet<CategoryVacanciesList> CategoryVacanciesList { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryVacanciesList>()
                .Property(e => e.CategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<CategoryVacanciesList>()
                .Property(e => e.CategoryLink)
                .IsUnicode(false);
        }
    }
}
