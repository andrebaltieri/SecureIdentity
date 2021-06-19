# How to use
SecureIdentity current provides only password generation and hashing. More features will be added later.

## Install
```
dotnet add package SecureIdentity
```

## Strong password generator
You can use `PasswordGenerator.Generate` method to generate strong passwords. By default it will generate a 16 characters long password including special chars.

```csharp
var password = PasswordGenerator.Generate();
// Default samples
// 'Byp%%NBb+5Ps[;7
// zBAZ8;9<XpH/>g}L
// ;k65YHa,~P&VYqSQ
```

You can specify some parameters to change the default behaviour:
```csharp
var password = PasswordGenerator.Generate(lenght: 25, includeSpecialChars: true, upperCase: true);
```
* **Lenght** is the password size in characters
* **Include Special Chars** will include special characters in password, like `!@#$%ˆ&*(){}[];`
* **Upper Case** will automatically cast the password (string) to uppercase

## Password Hasher
The `PasswordHasher.Hash` method will generate a hash from your password (string).

```csharp
var passHash = PasswordHasher.Hash("YOUR_PASSWORD");
```

By default, `PasswordHasher` will use the default parameters, having 1000 iterations an using `.` to separate the salt from iterations value on hash.

```
short saltSize = 16
short keySize = 32
int iterations = 10000
char splitChar = '.'
```

## Verify hash
You can use `PasswordHasher.Verify` method to verify a hash against a password (string).

```csharp
var result = PasswordHasher.Verify("YOUR_HASH", "YOUR_PASSWORD");
```

The result of this method is a boolean, meaning `true` is a match (Valid hash).

# Real World usage
Do not save pure string password on your database, instead, hash it! So before save your user account, you can do:

```csharp
public async Task<IActionResult> Post([FromBody]User user)
{
    ...
    user.Password = PasswordHasher.Hash(user.Password);    
    await context.Users.AddAsync(user);
    await context.SaveChangesAsync();
    ...
}
```
Or do not ask for a password at first time (Account creation) and generate a strong one:

```csharp
public async Task<IActionResult> CreateAccount([FromBody]User user)
{
    ...
    var password = PasswordGenerator.Generate();
    user.Password = PasswordHasher.Hash(password);

    await context.Users.AddAsync(user);
    await context.SaveChangesAsync();

    SendPasswordToUserEmail(password);
    ...
}
```

Now we have a hash saved on our database, something like this:
```
10000.qkz490QlOBQ+qMzsN7oXJg==.46dj2+bgMcg9RD37qK+QWeM37mY4MeBsAt5jwLf1orM=
```

As you may guess, to login, we`ll receive a pure password from our user and we need to hash it before go to database.

```csharp
public async Task<IActionResult> Login([FromBody]LoginViewModel model)
{
    ...
    // Get user from database
    var user = await context.Users.FirstOrDefaultAsync(x=>x.Email == model.Email);
    
    // Hash the pure password we received
    var hash = PasswordHasher.Hash(model.Password);

    // Verify the hash
    var isValid = PasswordHasher.Verify(hash, user.Password);
    ...
}
```