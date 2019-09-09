namespace AvenueOne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSuffixToPersonModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "Suffix", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "Suffix");
        }
    }
}
