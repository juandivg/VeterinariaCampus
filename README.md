# VeterinariaCampus
Aca esta un listado de los endpoints de las consultas, para hacer las consultas, el usuario debe tener el rol administrator y estar autenticado:

## Consulta 1

-Crear un consulta que permita visualizar los veterinarios cuya especialidad sea Cirujano vascular:
-Endpoint: api/Veterinario/GetVeterinariosEspecialidad/{especialidad}

## Consulta 2

-Listar los medicamentos que pertenezcan a el laboratorio Genfar:
-Endpoint: api/Medicamento/GetMedicamentosLaboratorio/{laboratorio}

## Consulta 3

-Mostrar las mascotas que se encuentren registradas cuya especie sea felina: 
-Endpoint: api/Mascota/GetMascotasEspecie/{especie}

## Consulta 4

-Listar los propietarios y sus mascotas.
-Endpoint: api/Propietario/GetPropietarioxMascotas

## Consulta 5

-Listar los medicamentos que tenga un precio de venta mayor a 50000
-Endpoint: api/Medicamento/GetMedicamentosxPrecio/{precio}

## Consulta 6

-Listar las mascotas que fueron atendidas por motivo de vacunacion en el primer trimestre del 2023
-Endpoint: api/Cita/GetMascotasCita/{motivo}&{fechainicio}&{fechafinal}

## Consulta 7

-Listar todas las mascotas agrupadas por especie.
-Endpoint: api/Especie/GetEspeciexMascotas

## Consulta 8

-Listar todos los movimientos de medicamentos y el valor total de cada movimiento.
-Endpoint: api/Movimiento/GetMovimientos

## Consulta 9

-Listar las mascotas que fueron atendidas por un determinado veterinario.
-Endpoint: api/Cita/GetMascotasVeterinario/{veterinario}

## Consulta 10

-Listar los proveedores que me venden un determinado medicamento.
-Endpoint: api/Proveedor/GetProveedoresxMedicamento/{medicamento}

## Consulta 11

-Listar las mascotas y sus propietarios cuya raza sea Golden Retriver
-Endpoint: api/Propietario/GetPropietarioxMascotasRaza/{raza}

## Consulta 12

-Listar la cantidad de mascotas que pertenecen a una raza a una raza. Nota: Se debe mostrar una lista de las razas y la cantidad de mascotas que pertenecen a la raza.
-Endpoint: api/Raza/GetRazasxMascotas
