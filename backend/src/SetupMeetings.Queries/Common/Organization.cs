using System;

namespace SetupMeetings.Queries.Common
{
    public class Organization
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public static Organization FindById(Guid organizationId)
        {
            return new Organization()
            {
                Id = organizationId,
                Name = "株式会社 とりあえず"
            };
        }
    }
}
