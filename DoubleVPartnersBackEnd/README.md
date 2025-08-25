


dotnet ef migrations add Start -p ./DoubleVPartners.BackEnd.Infrastructure -s ./DoubleVPartners.BackEnd.Api -c PgDbContext -o Migrations/PgDbsContext

dotnet ef database update -p ./DoubleVPartners.BackEnd.Infrastructure -s ./DoubleVPartners.BackEnd.Api -c PgDbContext