CREATE TABLE IF NOT EXISTS "Roles" (
    "Id" INT PRIMARY KEY NOT NULL,
    "Name" VARCHAR(100) NOT NULL UNIQUE,
    "IsActive" BOOLEAN NOT NULL DEFAULT TRUE,
    "Created" TIMESTAMP NOT NULL DEFAULT NOW(),
    "Updated" TIMESTAMP,
    "Deleted" TIMESTAMP,
    "TenantId" UUID NOT NULL DEFAULT gen_random_uuid()
);

CREATE TABLE IF NOT EXISTS "Permissions" (
    "Id" INT PRIMARY KEY NOT NULL,
    "Name" VARCHAR(100) NOT NULL UNIQUE,
    "Description" VARCHAR(255) NOT NULL,
    "IsActive" BOOLEAN NOT NULL DEFAULT TRUE,
    "Created" TIMESTAMP NOT NULL DEFAULT NOW(),
    "Updated" TIMESTAMP,
    "Deleted" TIMESTAMP,
    "TenantId" UUID NOT NULL DEFAULT gen_random_uuid()
);

CREATE TABLE IF NOT EXISTS "Users" (
    "Id" UUID PRIMARY KEY NOT NULL DEFAULT gen_random_uuid(),
    "FirstNames" VARCHAR(255) NOT NULL,
    "LastName" VARCHAR(255) NOT NULL,
    "ShortName" VARCHAR(100) NOT NULL,
    "Code" VARCHAR(100) NOT NULL UNIQUE,
    "LMSId" INT NOT NULL UNIQUE,
    "ImageUrl" VARCHAR(255),
    "Address" VARCHAR(255) NOT NULL,
    "PhoneNumber" VARCHAR(50) NOT NULL,
    "Email" VARCHAR(255) NOT NULL UNIQUE,
    "Password" VARCHAR(255) NOT NULL,
    "Gender" CHAR NOT NULL,
    "BirthDate" DATE NOT NULL,
    "RoleId" INT NOT NULL,
    "IsActive" BOOLEAN NOT NULL DEFAULT TRUE,
    "Created" TIMESTAMP NOT NULL DEFAULT NOW(),
    "Updated" TIMESTAMP,
    "Deleted" TIMESTAMP,
    "TenantId" UUID NOT NULL DEFAULT gen_random_uuid(),
    FOREIGN KEY ("RoleId") REFERENCES "Roles"("Id") ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS "RolePermissions" (
    "Id" UUID PRIMARY KEY NOT NULL DEFAULT gen_random_uuid(),
    "RoleId" INT NOT NULL,
    "PermissionId" INT NOT NULL,
    "IsActive" BOOLEAN NOT NULL DEFAULT TRUE,
    "Created" TIMESTAMP NOT NULL DEFAULT NOW(),
    "Updated" TIMESTAMP,
    "Deleted" TIMESTAMP,
    "TenantId" UUID NOT NULL DEFAULT gen_random_uuid(),
    FOREIGN KEY ("RoleId") REFERENCES "Roles"("Id") ON DELETE CASCADE,
    FOREIGN KEY ("PermissionId") REFERENCES "Permissions"("Id") ON DELETE CASCADE
);
