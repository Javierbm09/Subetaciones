update TransformadoresSubtransmision set voltajeimpulso = (select voltaje from voltajessistemas 
where TransformadoresSubtransmision.voltajeimpulso = voltajessistemas.id_voltajeSistema)


update TransformadoresTransmision set voltajeimpulso = (select voltaje from voltajessistemas 
where TransformadoresTransmision.voltajeimpulso = voltajessistemas.id_voltajeSistema)

