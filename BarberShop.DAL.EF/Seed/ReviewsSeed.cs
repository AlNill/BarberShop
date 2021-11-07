using BarberShop.DAL.Common.Models;

namespace BarberShop.DAL.EF.Seed
{
    public static class ReviewsSeed
    {
        public static Review[] SeedReviews()
        {
            Review review1 = new Review() {Id = 1, BarberId = 1, UserId = 2, 
                UserReview = "I moved from the Twin Cities and was looking for an actual Barber " +
                             "vs salon that calls themselves barbers. There are only 2 shops in " +
                             "La X that are legit Barbers. I go to Barber Joe in Valley View " +
                             "Mall. The guy is phenomenal! Can book the appointment online and " +
                             "he has a little kiosk style store by the food court. I can not " +
                             "say enough good things about him. Check him out."
            };
            Review review2 = new Review() {Id = 2, BarberId = 2, UserId = 3, 
                UserReview = "It was our first time using Barber Petr. My sons never had a " +
                             "haircut he’s loved, until now! Great work, fast, efficient and " +
                             "friendly! We will be back!"
            };
            Review review3 = new Review() {Id = 3, BarberId = 1, UserId = 5, 
                UserReview = "A great haircut at a great price. Conversation with Joe is " +
                             "always easy and fun. Highly recommended"
            };
            Review review4 = new Review() {Id = 4, BarberId = 3, UserId = 7, 
                UserReview = "Barber Mark is highly professional. Will definitely recommend."
            };
            Review review5 = new Review() {Id = 5, BarberId = 2, UserId = 12, 
                UserReview = "Barber Petr is awesome! He treats you like a person and not a " +
                             "hair trimmer number like commercial shops. He knows his trade " +
                             "and is extremely skilled. He even asks how your last haircut " +
                             "worked out and grew out to make the next one even better. " +
                             "Do yourself a favor and visit Barber Petr. He is the real deal, " +
                             "and you'll be glad you did!"
            };

            return new Review[] { review1, review2, review3, review4, review5};
        }
    }
}
