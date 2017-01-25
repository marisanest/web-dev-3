namespace Beleg3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTodos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TodoModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titel = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TodoModels");
        }
    }
}
