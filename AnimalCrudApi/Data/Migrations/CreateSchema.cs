using FluentMigrator;

namespace AnimalCrudApi.Data.Migrations
{
    [Migration(19032024)]
    public class CreateSchema : Migration
    {
        public override void Down()
        {
          
        }

        public override void Up()
        {
            Create.Table("animal")
                 .WithColumn("id").AsInt32().PrimaryKey().Identity()
                  .WithColumn("name").AsString(128).NotNullable()
                   .WithColumn("age").AsInt32().NotNullable()
                    .WithColumn("weight").AsInt32().NotNullable();
        }
    }
}
