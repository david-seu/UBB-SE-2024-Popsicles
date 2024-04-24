using UBB_SE_2024_Popsicles.Models;
using UBB_SE_2024_Popsicles.Repositories;

namespace TestPopsicles
{
    public class Tests
    {
        private GroupMemberRepository testGroupMemberRepository;
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestAdd()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string username = "testUserName";
            string password = "password";
            string email = "email";
            string phone = "0000000323";
            string description = "description";

            GroupMember testGroupMember = new GroupMember(id, username, password, email, phone, description);

            // Act
            testGroupMemberRepository.AddGroupMember(testGroupMember);

            // Assert
            List<GroupMember> members = testGroupMemberRepository.GetGroupMembers();
            Assert.Equals(members.Contains(testGroupMember), "The added member was not found in the repository.");
        }
    }
}