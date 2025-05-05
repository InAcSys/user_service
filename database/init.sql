CREATE TABLE
    IF NOT EXISTS "Users" (
        "Id" UUID PRIMARY KEY NOT NULL DEFAULT gen_random_uuid (),
        "FirstNames" VARCHAR(255) NOT NULL,
        "LastNames" VARCHAR(255) NOT NULL,
        "ShortName" VARCHAR(100) NOT NULL,
        "Code" VARCHAR(100) NOT NULL UNIQUE,
        "LMSId" INT NOT NULL UNIQUE,
        "CI" VARCHAR(20) NOT NULL UNIQUE,
        "CIType" VARCHAR(20) NOT NULL,
        "ImageUrl" VARCHAR(255),
        "Address" VARCHAR(255) NOT NULL,
        "PhoneNumber" VARCHAR(50) NOT NULL,
        "Email" VARCHAR(255) NOT NULL UNIQUE,
        "Password" VARCHAR(255) NOT NULL,
        "Gender" CHAR NOT NULL,
        "BirthDate" DATE NOT NULL,
        "RoleId" INT NOT NULL,
        "Salt" BYTEA NOT NULL,
        "IsActive" BOOLEAN NOT NULL DEFAULT TRUE,
        "Created" TIMESTAMP NOT NULL DEFAULT NOW (),
        "Updated" TIMESTAMP,
        "Deleted" TIMESTAMP,
        "TenantId" UUID NOT NULL DEFAULT gen_random_uuid ()
    );
    