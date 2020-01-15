namespace AvenueOne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerAndUserCascadeOnDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "PersonId", "dbo.People");
            DropForeignKey("dbo.Users", "PersonId", "dbo.People");
            AddForeignKey("dbo.Customers", "PersonId", "dbo.People", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Users", "PersonId", "dbo.People", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "PersonId", "dbo.People");
            DropForeignKey("dbo.Customers", "PersonId", "dbo.People");
            AddForeignKey("dbo.Users", "PersonId", "dbo.People", "Id");
            AddForeignKey("dbo.Customers", "PersonId", "dbo.People", "Id");
        }
    }
}
