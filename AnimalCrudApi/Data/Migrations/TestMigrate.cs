using FluentMigrator;

namespace AnimalCrudApi.Data.Migrations
{
    [Migration(19032024427)]
    public class TestMigrate:Migration
    {
        public override void Up()
        {
            Execute.Script(@"./Data/Scripts/data.sql");
        }
        public override void Down()
        {
            throw new NotImplementedException();
        }

    }
}
