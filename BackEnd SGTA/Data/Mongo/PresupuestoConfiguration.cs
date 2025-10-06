using MongoDB.Bson.Serialization;
using BackEndSGTA.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;

namespace BackEndSGTA.Data.Mongo;

public static class PresupuestoConfiguration
{
        public static void Register()
        {
                if (!BsonClassMap.IsClassMapRegistered(typeof(Presupuesto)))
                {
                        BsonClassMap.RegisterClassMap<Presupuesto>(cm =>
                        {
                                cm.AutoMap();
                                cm.MapIdField(c => c._id)
                                  .SetElementName("_id")
                                  .SetSerializer(new StringSerializer(BsonType.ObjectId));

                                cm.MapMember(c => c.Fecha)
                                  .SetElementName("fecha")
                                  .SetSerializer(new DateTimeSerializer(BsonType.String));
                                cm.MapMember(c => c.IdCliente).SetElementName("idCliente");
                                cm.MapMember(c => c.Cliente).SetElementName("cliente");
                                cm.MapMember(c => c.Domicilio).SetElementName("domicilio");
                                cm.MapMember(c => c.Poliza).SetElementName("poliza");
                                cm.MapMember(c => c.IdVehiculo).SetElementName("idVehiculo");
                                cm.MapMember(c => c.Vehiculo).SetElementName("vehiculo");
                                cm.MapMember(c => c.Patente).SetElementName("patente");
                                cm.MapMember(c => c.Siniestro).SetElementName("siniestro");
                                cm.MapMember(c => c.ManoDeObraChapa).SetElementName("manoDeObraChapa");
                                cm.MapMember(c => c.ManoDeObraPintura).SetElementName("manoDeObraPintura");
                                cm.MapMember(c => c.Mecanica).SetElementName("mecanica");
                                cm.MapMember(c => c.Electricidad).SetElementName("electricidad");
                                cm.MapMember(c => c.TotalRepuestos).SetElementName("totalRepuestos");
                                cm.MapMember(c => c.Total).SetElementName("total");
                                cm.MapMember(c => c.Items).SetElementName("items");
                                cm.MapMember(c => c.FirmaCliente).SetElementName("firmaCliente");
                                cm.MapMember(c => c.FirmaResponsable).SetElementName("firmaResponsable");
                                cm.MapMember(c => c.LugarFecha).SetElementName("lugarFecha");
                                cm.MapMember(c => c.Observaciones).SetElementName("observaciones");
                                cm.MapMember(c => c.RuedaAuxilio).SetElementName("ruedaAuxilio");
                                cm.MapMember(c => c.Encendedor).SetElementName("encendedor");
                                cm.MapMember(c => c.Cricket).SetElementName("cricket");
                                cm.MapMember(c => c.Herramientas).SetElementName("herramientas");
                        });
                }

                if (!BsonClassMap.IsClassMapRegistered(typeof(PresupuestoItem)))
                {
                        BsonClassMap.RegisterClassMap<PresupuestoItem>(cm =>
                        {
                                cm.AutoMap();
                                cm.MapMember(c => c.Id).SetElementName("id");
                                cm.MapMember(c => c.Ubicacion).SetElementName("ubicacion");
                                cm.MapMember(c => c.Descripcion).SetElementName("descripcion");
                                cm.MapMember(c => c.A).SetElementName("a");
                                cm.MapMember(c => c.B).SetElementName("b");
                                cm.MapMember(c => c.Observaciones).SetElementName("observaciones");
                                cm.MapMember(c => c.Importe).SetElementName("importe");
                        });
                }
        }
}

