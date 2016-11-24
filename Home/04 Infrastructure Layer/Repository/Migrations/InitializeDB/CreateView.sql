create view EquipmentView
as
 
select   a1.attvalue 'make' ,a2.attvalue 'model',count(0) 'count' from (select * from photoattribute where attkey='EquipmentMake') as  a1 ,
    (select * from photoattribute where attkey='EquipmentModel') as  a2
    where a1.photoid=a2.photoid  
    group by a2.attvalue ,a1.attvalue 
   