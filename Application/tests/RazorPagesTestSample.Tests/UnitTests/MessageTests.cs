using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using RazorPagesTestSample.Data;
using System.ComponentModel.DataAnnotations;
using System;

namespace RazorPagesTestSample.Tests.UnitTests
{
    public class MessageTests
    {
        [Fact]
        public void MessageLengthValidation()
        {
            using (var db = new AppDbContext(Utilities.TestDbContextOptions()))
            {
                // Arrange
                var chars250 = Enumerable.Range(0, 250).Select(i => (char)i).ToArray();
                var chars251 = Enumerable.Range(0, 251).Select(i => (char)i).ToArray();

                var safeMessage = new Message()
                {
                    Id = 1,
                    Text = new string(chars250)
                };

                var unsafeMessage = new Message()
                {
                    Id = 2,
                    Text = new string(chars251)
                };

                // Act
                var resultSafe = Validator.TryValidateObject(safeMessage, new ValidationContext(safeMessage, null, null), null, true);
                var resultUnsafe = Validator.TryValidateObject(unsafeMessage, new ValidationContext(unsafeMessage, null, null), null, true);

                // Assert
                Assert.True(resultSafe);
                Assert.False(resultUnsafe);
            }
        }
    }
}
