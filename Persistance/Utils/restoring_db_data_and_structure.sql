create table Category2
(
	Id integer
		primary key autoincrement,
	Name TEXT not null,
	Description TEXT,
	ParentId BIGINT
		references Category2
ON DELETE cascade
);

create table Field2
(
	Id integer not null
		constraint Field_pk2
			primary key autoincrement,
	Name text not null,
	Description text
);

create unique index Field_Id2_uindex
	on Field2 (Id);

create table CategoryField2
(
	CategoryId integer not null
		references Category2,
	FieldId integer not null
		references Field2,
	Value text not null,
	constraint CategoryField_pk2
		primary key (CategoryId, FieldId)
);

delete from CategoryField
where CategoryId + '-' + FieldId in
     (
select cf.CategoryId + '-' + cf.FieldId
from CategoryField cf
left join Category C on cf.CategoryId = C.Id
left join Field F on cf.FieldId = F.Id
where c.Name is null or f.Name is null
    )

insert into Field2
select * from Field;

insert into Category2
select * from Category;

insert into CategoryField2
select * from CategoryField;

drop table CategoryField;
drop table Field;
drop table Category;

create table Category
(
	Id integer
		primary key autoincrement,
	Name TEXT not null,
	Description TEXT,
	ParentId BIGINT
		references Category
ON DELETE cascade
);

create table Field
(
	Id integer not null
		constraint Field_pk
			primary key autoincrement,
	Name text not null,
	Description text
);

create unique index Field_Id_uindex
	on Field (Id);

create table CategoryField
(
	CategoryId integer not null
		references Category,
	FieldId integer not null
		references Field,
	Value text not null,
	constraint CategoryField_pk
		primary key (CategoryId, FieldId)
);

insert into Category
select * from Category2;

insert into Field
select * from Field2;

insert into CategoryField
select * from CategoryField2;

drop table CategoryField2;
drop table Field2;
drop table Category2;