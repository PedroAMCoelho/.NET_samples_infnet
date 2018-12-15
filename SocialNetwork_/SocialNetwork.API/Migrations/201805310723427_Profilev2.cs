namespace SocialNetwork.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Profilev2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Profiles", newName: "UProfiles");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.UProfiles", newName: "Profiles");
        }
    }
}
