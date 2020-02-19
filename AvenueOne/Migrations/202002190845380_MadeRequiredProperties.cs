namespace AvenueOne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeRequiredProperties : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transactions", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Transactions", new[] { "Customer_Id" });
            AlterColumn("dbo.Transactions", "Customer_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Transactions", "Customer_Id");
            AddForeignKey("dbo.Transactions", "Customer_Id", "dbo.Customers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Transactions", new[] { "Customer_Id" });
            AlterColumn("dbo.Transactions", "Customer_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Transactions", "Customer_Id");
            AddForeignKey("dbo.Transactions", "Customer_Id", "dbo.Customers", "Id");
        }
    }
}
