create OR REPLACE view EquipmentView
as
 
select   a1.attvalue 'make' ,a2.attvalue 'model',count(0) 'count' from photoattribute  as  a1 
 inner  join   photoattribute as  a2 on 
      a1.OwnerID=a2.OwnerID  where   a1.attkey='EquipmentMake'and a2.attkey='EquipmentModel'
    group by a2.attvalue ,a1.attvalue 
   