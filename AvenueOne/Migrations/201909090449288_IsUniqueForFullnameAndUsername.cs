namespace AvenueOne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsUniqueForFullnameAndUsername : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Username", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.People", "FirstName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.People", "MiddleName", c => c.String(maxLength: 100));
            AlterColumn("dbo.People", "LastName", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.People", "MaidenName", c => c.String(maxLength: 100));
            AlterColumn("dbo.People", "FullName", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.Users", "Username", unique: true, name: "Username");
            CreateIndex("dbo.People", "FullName", unique: true, name: "FullName");
        }
        
        public override void Down()
        {
            DropIndex("dbo.People", "FullName");
            DropIndex("dbo.Users", "Username");
            AlterColumn("dbo.People", "FullName", c => c.String());
            AlterColumn("dbo.People", "MaidenName", c => c.String());
            AlterColumn("dbo.People", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.People", "MiddleName", c => c.String());
            AlterColumn("dbo.People", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Username", c => c.String(nullable: false));
        }
    }
}
