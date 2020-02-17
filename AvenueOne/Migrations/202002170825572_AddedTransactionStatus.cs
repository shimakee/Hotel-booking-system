namespace AvenueOne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTransactionStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "Status", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "Status");
        }
    }
}
