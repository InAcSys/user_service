CREATE TABLE
    IF NOT EXISTS "Roles" (
        "Id" SERIAL PRIMARY KEY,
        "Name" VARCHAR(100) NOT NULL UNIQUE,
        "IsActive" BOOLEAN NOT NULL DEFAULT TRUE,
        "Created" TIMESTAMP NOT NULL DEFAULT NOW (),
        "Updated" TIMESTAMP,
        "Deleted" TIMESTAMP,
        "TenantId" UUID NOT NULL DEFAULT gen_random_uuid ()
    );

CREATE TABLE
    IF NOT EXISTS "Permissions" (
        "Id" SERIAL PRIMARY KEY,
        "Name" VARCHAR(100) NOT NULL UNIQUE,
        "Description" VARCHAR(255) NOT NULL,
        "IsActive" BOOLEAN NOT NULL DEFAULT TRUE,
        "Created" TIMESTAMP NOT NULL DEFAULT NOW (),
        "Updated" TIMESTAMP,
        "Deleted" TIMESTAMP,
        "TenantId" UUID NOT NULL DEFAULT gen_random_uuid ()
    );

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
        "IsActive" BOOLEAN NOT NULL DEFAULT TRUE,
        "Created" TIMESTAMP NOT NULL DEFAULT NOW (),
        "Updated" TIMESTAMP,
        "Deleted" TIMESTAMP,
        "TenantId" UUID NOT NULL DEFAULT gen_random_uuid (),
        FOREIGN KEY ("RoleId") REFERENCES "Roles" ("Id") ON DELETE CASCADE
    );

CREATE TABLE
    IF NOT EXISTS "RolePermissions" (
        "Id" UUID PRIMARY KEY NOT NULL DEFAULT gen_random_uuid (),
        "RoleId" INT NOT NULL,
        "PermissionId" INT NOT NULL,
        "IsActive" BOOLEAN NOT NULL DEFAULT TRUE,
        "Created" TIMESTAMP NOT NULL DEFAULT NOW (),
        "Updated" TIMESTAMP,
        "Deleted" TIMESTAMP,
        "TenantId" UUID NOT NULL DEFAULT gen_random_uuid (),
        FOREIGN KEY ("RoleId") REFERENCES "Roles" ("Id") ON DELETE CASCADE,
        FOREIGN KEY ("PermissionId") REFERENCES "Permissions" ("Id") ON DELETE CASCADE
    );

INSERT INTO
    "Roles" ("Name")
VALUES
    ('Estudiante'),
    ('Docente'),
    ('Admin'),
    ('Director'),
    ('Padre de familia');

INSERT INTO
    "Permissions" ("Name", "Description")
VALUES
    ('Crear usuario', 'Permite crear un nuevo usuario'),
    (
        'Actualizar usuario',
        'Permite actualizar un usuario existente'
    ),
    ('Eliminar usuario', 'Permite eliminar un usuario'),
    (
        'Leer usuario',
        'Permite ver los detalles de un usuario'
    ),
    ('Crear rol', 'Permite crear un nuevo rol'),
    (
        'Actualizar rol',
        'Permite actualizar un rol existente'
    ),
    ('Eliminar rol', 'Permite eliminar un rol'),
    ('Leer rol', 'Permite ver los detalles de un rol'),
    (
        'Asignar rol',
        'Permite asignar un rol a un usuario'
    );