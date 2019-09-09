namespace AvenueOne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PersonUserRelationshipOptional : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.People", "Id", "dbo.Users");
            DropIndex("dbo.People", new[] { "Id" });
            AddColumn("dbo.Users", "PersonId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Users", "PersonId");
            AddForeignKey("dbo.Users", "PersonId", "dbo.People", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "PersonId", "dbo.People");
            DropIndex("dbo.Users", new[] { "PersonId" });
            DropColumn("dbo.Users", "PersonId");
            CreateIndex("dbo.People", "Id");
            AddForeignKey("dbo.People", "Id", "dbo.Users", "Id");
        }
    }
}
