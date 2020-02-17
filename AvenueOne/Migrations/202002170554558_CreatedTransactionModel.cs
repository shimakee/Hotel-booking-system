namespace AvenueOne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedTransactionModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DateAdded = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                        Customer_Id = c.String(maxLength: 128),
                        Employee_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .ForeignKey("dbo.Users", t => t.Employee_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.Employee_Id);
            
            AddColumn("dbo.Bookings", "Transaction_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Bookings", "Transaction_Id");
            AddForeignKey("dbo.Bookings", "Transaction_Id", "dbo.Transactions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "Employee_Id", "dbo.Users");
            DropForeignKey("dbo.Transactions", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Bookings", "Transaction_Id", "dbo.Transactions");
            DropIndex("dbo.Transactions", new[] { "Employee_Id" });
            DropIndex("dbo.Transactions", new[] { "Customer_Id" });
            DropIndex("dbo.Bookings", new[] { "Transaction_Id" });
            DropColumn("dbo.Bookings", "Transaction_Id");
            DropTable("dbo.Transactions");
        }
    }
}
