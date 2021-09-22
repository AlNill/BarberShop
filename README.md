# For build app:
  1. Delete old migrations
  2. Delete old database
# For use:
  1. Seed two user - admin and user
      Credentials for admin:
        Login: Admin
        Password: Admin
      Credentials for user:
        Login: User
        Password: User
  2. Admin has full privileges (Add or edit barber, see list of users, add review...)
  3. User can only see list of barbers, make record, add review.

# Angular use
  1. Use Package Manager Console and cd to BarberShop.Web/ClientApp
  2. Type command npm install
  3. Start project BarberShop with BarberShop.Web as startup project 
  4. Go to browser and type https://localhost:44395/barbers
  5. Wait few seconds ;)
