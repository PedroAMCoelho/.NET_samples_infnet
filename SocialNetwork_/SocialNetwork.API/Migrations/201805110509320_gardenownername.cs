namespace SocialNetwork.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gardenownername : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "GardenOwnerName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "GardenOwnerName");
        }
    }
}
