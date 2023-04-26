declare @cant int
declare @numaccion int
declare @EstAdminProv smallint

Set @EstAdminProv =  ([dbo].[EstructuraAdminProvincial]());
 

select  @cant=count(Nombre) from Fabricantes where Nombre='ABB' and pais='Suiza'
if (@cant=0)
begin
 Exec GetNumAccion @EstAdminProv,'I','IEF',1,0,0,@numaccion Output  
          
 insert into Fabricantes  (Nombre,Pais,EstructuraAdministrativa,NumAccion,FTransformadoresSub) values 
 ('ABB','Suiza',@EstAdminProv,@numaccion,1)
end
else
begin
 update Fabricantes set FTransformadoresSub=1 where Nombre='ABB' and pais='Suiza'
end

select @cant=count(Nombre) from Fabricantes where Nombre='TUR' and pais='Alemania'
if (@cant=0)
begin
 Exec GetNumAccion @EstAdminProv,'I','IEF',1,0,0,@numaccion Output  
          
 insert into Fabricantes  (Nombre,Pais,EstructuraAdministrativa,NumAccion,FTransformadoresSub) values 
 ('TUR','Alemania',@EstAdminProv,@numaccion,1)
end
else
begin
 update Fabricantes set FTransformadoresSub=1 where Nombre='TUR' and pais='Alemania'
end


select @cant=count(Nombre) from Fabricantes where Nombre='ALSTOM' and pais='Francia'
if (@cant=0)
begin
 Exec GetNumAccion @EstAdminProv,'I','IEF',1,0,0,@numaccion Output  
          
 insert into Fabricantes  (Nombre,Pais,EstructuraAdministrativa,NumAccion,FTransformadoresSub) values 
 ('ALSTOM','Francia',@EstAdminProv,@numaccion,1)
end
else
begin
 update Fabricantes set FTransformadoresSub=1 where Nombre='ALSTOM' and pais='Francia'
end


select @cant=count(Nombre) from Fabricantes where Nombre='EGB' and pais='Austria'
if (@cant=0)
begin
 Exec GetNumAccion @EstAdminProv,'I','IEF',1,0,0,@numaccion Output  
          
 insert into Fabricantes  (Nombre,Pais,EstructuraAdministrativa,NumAccion,FTransformadoresSub) values 
 ('EGB','Austria',@EstAdminProv,@numaccion,1)
end
else
begin
 update Fabricantes set FTransformadoresSub=1 where Nombre='EGB' and pais='Austria'
end
 
select @cant=count(Nombre) from Fabricantes where Nombre='ELPROM' and pais='Bulgaria'
if (@cant=0)
begin
 Exec GetNumAccion @EstAdminProv,'I','IEF',1,0,0,@numaccion Output  
          
 insert into Fabricantes  (Nombre,Pais,EstructuraAdministrativa,NumAccion,FTransformadoresSub) values 
 ('ELPROM','Bulgaria',@EstAdminProv,@numaccion,1)
end
else
begin
 update Fabricantes set FTransformadoresSub=1 where Nombre='ELPROM' and pais='Bulgaria'
end

select @cant=count(Nombre) from Fabricantes where Nombre='BOUDARY' and pais='Canadá'
if (@cant=0)
begin
 Exec GetNumAccion @EstAdminProv,'I','IEF',1,0,0,@numaccion Output  
          
 insert into Fabricantes  (Nombre,Pais,EstructuraAdministrativa,NumAccion,FTransformadoresSub) values 
 ('BOUDARY','Canadá',@EstAdminProv,@numaccion,1)
end
else
begin
 update Fabricantes set FTransformadoresSub=1 where Nombre='BOUDARY' and pais='Canadá'
end
 
select @cant=count(Nombre) from Fabricantes where Nombre='ETD' and pais='Checoslovaquia'
if (@cant=0)
begin
 Exec GetNumAccion @EstAdminProv,'I','IEF',1,0,0,@numaccion Output  
          
 insert into Fabricantes  (Nombre,Pais,EstructuraAdministrativa,NumAccion,FTransformadoresSub) values 
 ('ETD','Checoslovaquia',@EstAdminProv,@numaccion,1)
end
else
begin
 update Fabricantes set FTransformadoresSub=1 where Nombre='ETD' and pais='Checoslovaquia'
end
 
select @cant=count(Nombre) from Fabricantes where Nombre='SKODA' and pais='Checoslovaquia'
if (@cant=0)
begin
 Exec GetNumAccion @EstAdminProv,'I','IEF',1,0,0,@numaccion Output  
          
insert into Fabricantes  (Nombre,Pais,EstructuraAdministrativa,NumAccion,FTransformadoresSub) values 
 ('SKODA','Checoslovaquia',@EstAdminProv,@numaccion,1)
end
else
begin
 update Fabricantes set FTransformadoresSub=1 where Nombre='SKODA' and pais='Checoslovaquia'
end

select @cant=count(Nombre) from Fabricantes where Nombre='JSHP' and pais='China'
if (@cant=0)
begin
 Exec GetNumAccion @EstAdminProv,'I','IEF',1,0,0,@numaccion Output  
          
 insert into Fabricantes  (Nombre,Pais,EstructuraAdministrativa,NumAccion,FTransformadoresSub) values 
 ('JSHP','China',@EstAdminProv,@numaccion,1)
end
else
begin
 update Fabricantes set FTransformadoresSub=1 where Nombre='JSHP' and pais='China'
end
 
select @cant=count(Nombre) from Fabricantes where Nombre='SCE' and pais='China'
if (@cant=0)
begin
 Exec GetNumAccion @EstAdminProv,'I','IEF',1,0,0,@numaccion Output  
          
 insert into Fabricantes  (Nombre,Pais,EstructuraAdministrativa,NumAccion,FTransformadoresSub) values 
 ('SCE','China',@EstAdminProv,@numaccion,1)
end
else
begin
 update Fabricantes set FTransformadoresSub=1 where Nombre='SCE' and pais='China'
end

select @cant=count(Nombre) from Fabricantes where Nombre='TEBIAN' and pais='China'
if (@cant=0)
begin
 Exec GetNumAccion @EstAdminProv,'I','IEF',1,0,0,@numaccion Output  
          
 insert into Fabricantes  (Nombre,Pais,EstructuraAdministrativa,NumAccion,FTransformadoresSub) values 
 ('TEBIAN','China',@EstAdminProv,@numaccion,1)
end
else
begin
 update Fabricantes set FTransformadoresSub=1 where Nombre='TEBIAN' and pais='China'
end

select @cant=count(Nombre) from Fabricantes where Nombre='BEZ' and pais='Eslovaquia'
if (@cant=0)
begin
 Exec GetNumAccion @EstAdminProv,'I','IEF',1,0,0,@numaccion Output  
          
 insert into Fabricantes  (Nombre,Pais,EstructuraAdministrativa,NumAccion,FTransformadoresSub) values 
 ('BEZ','Eslovaquia',@EstAdminProv,@numaccion,1)
end
else
begin
 update Fabricantes set FTransformadoresSub=1 where Nombre='BEZ' and pais='Eslovaquia'
end

select @cant=count(Nombre) from Fabricantes where Nombre='ALKARGO' and pais='España'
if (@cant=0)
begin
 Exec GetNumAccion @EstAdminProv,'I','IEF',1,0,0,@numaccion Output  
          
 insert into Fabricantes  (Nombre,Pais,EstructuraAdministrativa,NumAccion,FTransformadoresSub) values 
 ('ALKARGO','España',@EstAdminProv,@numaccion,1)
end
else
begin
 update Fabricantes set FTransformadoresSub=1 where Nombre='ALKARGO' and pais='España'
end

select @cant=count(Nombre) from Fabricantes where Nombre='IMEFY' and pais='España'
if (@cant=0)
begin
 Exec GetNumAccion @EstAdminProv,'I','IEF',1,0,0,@numaccion Output  
          
 insert into Fabricantes  (Nombre,Pais,EstructuraAdministrativa,NumAccion,FTransformadoresSub) values 
 ('IMEFY','España',@EstAdminProv,@numaccion,1)
end
else
begin
 update Fabricantes set FTransformadoresSub=1 where Nombre='IMEFY' and pais='España'
end

select @cant=count(Nombre) from Fabricantes where Nombre='FRANCE TRANSFO' and pais='Francia'
if (@cant=0)
begin
 Exec GetNumAccion @EstAdminProv,'I','IEF',1,0,0,@numaccion Output  
          
 insert into Fabricantes  (Nombre,Pais,EstructuraAdministrativa,NumAccion,FTransformadoresSub) values 
 ('FRANCE TRANSFO','Francia',@EstAdminProv,@numaccion,1)
end
else
begin
 update Fabricantes set FTransformadoresSub=1 where Nombre='FRANCE TRANSFO' and pais='Francia'
end


select @cant=count(Nombre) from Fabricantes where Nombre='VTD' and pais='Italia'
if (@cant=0)
begin
 Exec GetNumAccion @EstAdminProv,'I','IEF',1,0,0,@numaccion Output  
          
 insert into Fabricantes  (Nombre,Pais,EstructuraAdministrativa,NumAccion,FTransformadoresSub) values 
 ('VTD','Italia',@EstAdminProv,@numaccion,1)
end
else
begin
 update Fabricantes set FTransformadoresSub=1 where Nombre='VTD' and pais='Italia'
end


 
select @cant=count(Nombre) from Fabricantes where Nombre='OSAKA' and pais='Japón'
if (@cant=0)
begin
 Exec GetNumAccion @EstAdminProv,'I','IEF',1,0,0,@numaccion Output  
          
 insert into Fabricantes  (Nombre,Pais,EstructuraAdministrativa,NumAccion,FTransformadoresSub) values 
 ('OSAKA','Japón',@EstAdminProv,@numaccion,1)
end
else
begin
 update Fabricantes set FTransformadoresSub=1 where Nombre='OSAKA' and pais='Japón'
end 

select @cant=count(Nombre) from Fabricantes where Nombre='MERLIN GERIN' and pais='Francia'
if (@cant=0)
begin
 Exec GetNumAccion @EstAdminProv,'I','IEF',1,0,0,@numaccion Output  
          
 insert into Fabricantes  (Nombre,Pais,EstructuraAdministrativa,NumAccion,FTransformadoresSub) values 
 ('MERLIN GERIN','Francia',@EstAdminProv,@numaccion,1)
end
else
begin
 update Fabricantes set FTransformadoresSub=1 where Nombre='MERLIN GERIN' and pais='Francia'
end 

select @cant=count(Nombre) from Fabricantes where Nombre='SCHNEIDER ELECTRIC' and pais='Francia'
if (@cant=0)
begin
 Exec GetNumAccion @EstAdminProv,'I','IEF',1,0,0,@numaccion Output  
          
 insert into Fabricantes  (Nombre,Pais,EstructuraAdministrativa,NumAccion,FTransformadoresSub) values 
 ('SCHNEIDER ELECTRIC','Francia',@EstAdminProv,@numaccion,1)
end
else
begin
 update Fabricantes set FTransformadoresSub=1 where Nombre='SCHNEIDER ELECTRIC' and pais='Francia'
end 

select @cant=count(Nombre) from Fabricantes where Nombre='SIEMEN' and pais='Alemania'
if (@cant=0)
begin
 Exec GetNumAccion @EstAdminProv,'I','IEF',1,0,0,@numaccion Output  
          
 insert into Fabricantes  (Nombre,Pais,EstructuraAdministrativa,NumAccion,FTransformadoresSub) values 
 ('SIEMEN','Alemania',@EstAdminProv,@numaccion,1)
end
else
begin
 update Fabricantes set FTransformadoresSub=1 where Nombre='SIEMEN' and pais='Alemania'
end 
 
select @cant=count(Nombre) from Fabricantes where Nombre='ELECKTROSRBIJA' and pais='Yugoslavia'
if (@cant=0)
begin
 Exec GetNumAccion @EstAdminProv,'I','IEF',1,0,0,@numaccion Output  
          
 insert into Fabricantes  (Nombre,Pais,EstructuraAdministrativa,NumAccion,FTransformadoresSub) values 
 ('ELECKTROSRBIJA','Yugoslavia',@EstAdminProv,@numaccion,1)
end
else
begin
 update Fabricantes set FTransformadoresSub=1 where Nombre='ELECKTROSRBIJA' and pais='Yugoslavia'
end 


 