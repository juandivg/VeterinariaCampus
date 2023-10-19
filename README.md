# VeterinariaCampus
Aca esta un listado de los endpoints de las consultas, para hacer las consultas, el usuario debe tener el rol administrator y estar autenticado:\
Crear un consulta que permita visualizar los veterinarios cuya especialidad sea Cirujano vascular:\
Endpoint: api/Veterinario/GetVeterinariosEspecialidad/{especialidad}\
Listar los medicamentos que pertenezcan a el laboratorio Genfar:
*Endpoint: api/Medicamento/GetMedicamentosLaboratorio/{laboratorio}
*Mostrar las mascotas que se encuentren registradas cuya especie sea felina:
*Endpoint: api/Mascota/GetMascotasEspecie/{especie}
*Listar los propietarios y sus mascotas.
*Endpoint: api/Propietario/GetPropietarioxMascotas
5.Listar los medicamentos que tenga un precio de venta mayor a 50000
*Endpoint: api/Medicamento/GetMedicamentosxPrecio/{precio}
6.Listar las mascotas que fueron atendidas por motivo de vacunacion en el primer trimestre del 2023
*Endpoint: api/Cita/GetMascotasCita/{motivo}&{fechainicio}&{fechafinal}
8.Listar todas las mascotas agrupadas por especie.
*Endpoint: api/Especie/GetEspeciexMascotas
9.Listar todos los movimientos de medicamentos y el valor total de cada movimiento.
*Endpoint: api/Movimiento/GetMovimientos
10.Listar las mascotas que fueron atendidas por un determinado veterinario.
*Endpoint: api/Cita/GetMascotasVeterinario/{veterinario}
11.Listar los proveedores que me venden un determinado medicamento.
*Endpoint: api/Proveedor/GetProveedoresxMedicamento/{medicamento}
12.Listar las mascotas y sus propietarios cuya raza sea Golden Retriver
*Endpoint: api/Propietario/GetPropietarioxMascotasRaza/{raza}
13.Listar la cantidad de mascotas que pertenecen a una raza a una raza. Nota: Se debe mostrar una lista de las razas y la cantidad de mascotas que pertenecen a la raza.
*Endpoint: api/Raza/GetRazasxMascotas
