namespace SocialNetwork.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Profile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        ProfileId = c.Int(nullable: false, identity: true),
                        PictureUrl = c.String(),
                        GardenName = c.String(),
                        GardenDescription = c.String(),
                        MainGarden = c.String(),
                        SubGarden = c.String(),
                        GardenLocation = c.String(),
                    })
                .PrimaryKey(t => t.ProfileId);
            
            AddColumn("dbo.AspNetUsers", "Profile_ProfileId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Profile_ProfileId");
            AddForeignKey("dbo.AspNetUsers", "Profile_ProfileId", "dbo.Profiles", "ProfileId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Profile_ProfileId", "dbo.Profiles");
            DropIndex("dbo.AspNetUsers", new[] { "Profile_ProfileId" });
            DropColumn("dbo.AspNetUsers", "Profile_ProfileId");
            DropTable("dbo.Profiles");
        }
    }
}
