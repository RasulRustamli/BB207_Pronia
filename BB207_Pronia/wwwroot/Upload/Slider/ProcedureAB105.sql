create table Students
(
Id int primary key identity,
Name nvarchar(50) not null,
Point decimal(5,2) not null
)







create procedure usp_GetStudentPoint @point int
as
select * from Students
where Students.Point>=@point


exec usp_GetStudentPoint 60



create procedure usp_GetStudentPointWhere @point1 int,@point2 int
as
select * from Students
where Students.Point>=@point1 and Students.Point<=@point2



exec usp_GetStudentPointWhere @point1=40,@point2=50