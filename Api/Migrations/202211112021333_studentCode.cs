namespace Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studentCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "StudentCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "StudentCode");
        }
    }
}
