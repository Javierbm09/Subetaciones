declare @cant int

select @cant=count(Voltaje) from VoltajesSistemas where Voltaje=230
if (@cant=0)
begin
 insert into VoltajesSistemas  (voltaje,Monofasico,Trifasico,Fuente,Servicio,Nivel,NC) values 
 (230,0,1,1,0,'A',1)
end

select @cant=count(Voltaje) from VoltajesSistemas where Voltaje=220
if (@cant=0)
begin
 insert into VoltajesSistemas  (voltaje,Monofasico,Trifasico,Fuente,Servicio,Nivel,NC) values 
 (220,0,1,1,0,'A',1)
end  

select @cant=count(Voltaje) from VoltajesSistemas where Voltaje=121
if (@cant=0)
begin
 insert into VoltajesSistemas  (voltaje,Monofasico,Trifasico,Fuente,Servicio,Nivel,NC) values 
 (121,0,1,1,0,'A',1)
end

select @cant=count(Voltaje) from VoltajesSistemas where Voltaje=115
if (@cant=0)
begin
 insert into VoltajesSistemas  (voltaje,Monofasico,Trifasico,Fuente,Servicio,Nivel,NC) values 
 (115,0,1,1,0,'A',1)
end

select @cant=count(Voltaje) from VoltajesSistemas where Voltaje=110
if (@cant=0)
begin
 insert into VoltajesSistemas  (voltaje,Monofasico,Trifasico,Fuente,Servicio,Nivel,NC) values 
 (110,0,1,1,0,'A',1)
end 

select @cant=count(Voltaje) from VoltajesSistemas where Voltaje=38.5
if (@cant=0)
begin
 insert into VoltajesSistemas  (voltaje,Monofasico,Trifasico,Fuente,Servicio,Nivel,NC) values 
 (38.5,0,1,1,0,'M',1)
end

select @cant=count(Voltaje) from VoltajesSistemas where Voltaje=34.5
if (@cant=0)
begin
 insert into VoltajesSistemas  (voltaje,Monofasico,Trifasico,Fuente,Servicio,Nivel,NC) values 
 (34.5,0,1,1,0,'M',1)
end   

select @cant=count(Voltaje) from VoltajesSistemas where Voltaje=33
if (@cant=0)
begin
 insert into VoltajesSistemas  (voltaje,Monofasico,Trifasico,Fuente,Servicio,Nivel,NC) values 
 (33,0,1,1,0,'M',1)
end

Update VoltajesSistemas set TransfSubPrimario=1 where 
voltaje= 230 or voltaje= 220 or voltaje= 121 or voltaje= 115 or
voltaje= 110 or voltaje= 38.5 or voltaje= 34.5 or voltaje= 33

----
 
select @cant=count(Voltaje) from VoltajesSistemas where Voltaje=13.8
if (@cant=0)
begin
 insert into VoltajesSistemas  (voltaje,Monofasico,Trifasico,Fuente,Servicio,Nivel,NC) values 
 (13.8,0,1,1,0,'M',1)
end

select @cant=count(Voltaje) from VoltajesSistemas where Voltaje=13.2
if (@cant=0)
begin
 insert into VoltajesSistemas  (voltaje,Monofasico,Trifasico,Fuente,Servicio,Nivel,NC) values 
 (13.2,0,1,1,0,'M',1)
end

select @cant=count(Voltaje) from VoltajesSistemas where Voltaje=10.5
if (@cant=0)
begin
 insert into VoltajesSistemas  (voltaje,Monofasico,Trifasico,Fuente,Servicio,Nivel,NC) values 
 (10.5,0,1,1,0,'M',1)
end

select @cant=count(Voltaje) from VoltajesSistemas where Voltaje=6.3
if (@cant=0)
begin
 insert into VoltajesSistemas  (voltaje,Monofasico,Trifasico,Fuente,Servicio,Nivel,NC) values 
 (6.3,0,1,1,0,'M',1)
end

select @cant=count(Voltaje) from VoltajesSistemas where Voltaje=4.33
if (@cant=0)
begin
 insert into VoltajesSistemas  (voltaje,Monofasico,Trifasico,Fuente,Servicio,Nivel,NC) values 
 (4.33,0,1,1,0,'M',1)
end

select @cant=count(Voltaje) from VoltajesSistemas where Voltaje=2.4
if (@cant=0)
begin
 insert into VoltajesSistemas  (voltaje,Monofasico,Trifasico,Fuente,Servicio,Nivel,NC) values 
 (2.4,0,1,1,0,'M',1)
end

select @cant=count(Voltaje) from VoltajesSistemas where Voltaje=2.4
if (@cant=0)
begin
 insert into VoltajesSistemas  (voltaje,Monofasico,Trifasico,Fuente,Servicio,Nivel,NC) values 
 (2.4,0,1,1,0,'M',1)
end

select @cant=count(Voltaje) from VoltajesSistemas where Voltaje=0.69
if (@cant=0)
begin
 insert into VoltajesSistemas  (voltaje,Monofasico,Trifasico,Fuente,Servicio,Nivel,NC) values 
 (0.69,0,1,1,0,'B',1)
end

select @cant=count(Voltaje) from VoltajesSistemas where Voltaje=0.48
if (@cant=0)
begin
 insert into VoltajesSistemas  (voltaje,Monofasico,Trifasico,Fuente,Servicio,Nivel,NC) values 
 (0.48,0,1,1,0,'B',1)
end

select @cant=count(Voltaje) from VoltajesSistemas where Voltaje=0.46
if (@cant=0)
begin
 insert into VoltajesSistemas  (voltaje,Monofasico,Trifasico,Fuente,Servicio,Nivel,NC) values 
 (0.46,0,1,1,0,'B',1)
end

select @cant=count(Voltaje) from VoltajesSistemas where Voltaje=0.44
if (@cant=0)
begin
 insert into VoltajesSistemas  (voltaje,Monofasico,Trifasico,Fuente,Servicio,Nivel,NC) values 
 (0.44,0,1,1,0,'B',1)
end

select @cant=count(Voltaje) from VoltajesSistemas where Voltaje=0.415
if (@cant=0)
begin
 insert into VoltajesSistemas  (voltaje,Monofasico,Trifasico,Fuente,Servicio,Nivel,NC) values 
 (0.415,0,1,1,0,'B',1)
end

select @cant=count(Voltaje) from VoltajesSistemas where Voltaje=0.4
if (@cant=0)
begin
 insert into VoltajesSistemas  (voltaje,Monofasico,Trifasico,Fuente,Servicio,Nivel,NC) values 
 (0.4,0,1,1,0,'B',1)
end      
 
Update VoltajesSistemas set TransfSubSecundario=1 where 
voltaje= 121 or voltaje= 110 or voltaje= 34.5 or voltaje= 13.8 or
voltaje= 13.2 or voltaje= 10.5 or voltaje= 6.3 or voltaje= 4.33 or
voltaje= 2.4 or voltaje= 0.69 or voltaje= 0.48 or voltaje= 0.46 or
voltaje= 0.44 or voltaje= 0.415 or voltaje= 0.4
 
-----

select @cant=count(Voltaje) from VoltajesSistemas where Voltaje=35
if (@cant=0)
begin
 insert into VoltajesSistemas  (voltaje,Monofasico,Trifasico,Fuente,Servicio,Nivel,NC) values 
 (35,0,1,1,0,'M',1)
end

select @cant=count(Voltaje) from VoltajesSistemas where Voltaje=6.6
if (@cant=0)
begin
 insert into VoltajesSistemas  (voltaje,Monofasico,Trifasico,Fuente,Servicio,Nivel,NC) values 
 (6.6,0,1,1,0,'M',1)
end


Update VoltajesSistemas set TransfSubTerciario=1 where 
voltaje= 35 or voltaje= 34.5 or voltaje= 6.6 or voltaje= 6.3

