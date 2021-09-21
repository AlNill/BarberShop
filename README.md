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
  1. You need node_modules in *\BarberShop.Front.Angular\front
  2. In directory invoke cmd.exe and run "npm start"
  3. Start project BarberShop with BarberShop.Web as startup project 
  4. Go to browser and type https://localhost:4200/barbers
