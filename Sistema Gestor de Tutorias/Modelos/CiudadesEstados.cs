using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Gestor_de_Tutorias.Modelos
{
    public class CiudadesEstados
    {
        public string Estado { get; set; }
        public string Capital { get; set; }

        public CiudadesEstados(string Estado, string Capital)
        {
            this.Estado = Estado;
            this.Capital = Capital;
        }

        public CiudadesEstados()
        {

        }

        public static List<CiudadesEstados> getCiudadesEstados()
        {
            var items = new List<CiudadesEstados>();
            items.Add(new CiudadesEstados() {Estado = "Aguascalientes", Capital = "Aguascalientes" });
            items.Add(new CiudadesEstados() {Estado = "Baja California", Capital = "Mexicali" });
            items.Add(new CiudadesEstados() {Estado = "Baja California Sur", Capital =  "La Paz" });
            items.Add(new CiudadesEstados() {Estado = "Campeche", Capital = "San Francisco de Campeche" });
            items.Add(new CiudadesEstados() {Estado = "Chihuahua", Capital = "Chihuahua" });
            items.Add(new CiudadesEstados() {Estado = "Ciudad de Mexico", Capital = "Distrito Federal" });
            items.Add(new CiudadesEstados() {Estado = "Chiapas", Capital = "Tuxtla Gutiérrez" });
            items.Add(new CiudadesEstados() {Estado = "Coahuila", Capital = "Saltillo" });
            items.Add(new CiudadesEstados() {Estado = "Colima", Capital = "Colima" });
            items.Add(new CiudadesEstados() {Estado = "Durango", Capital = "Victoria de Durango" });
            items.Add(new CiudadesEstados() {Estado = "Guanajuato", Capital = "Guanajuato" });
            items.Add(new CiudadesEstados() {Estado = "Guerrero", Capital = "Chilpancingo de los Bravo" });
            items.Add(new CiudadesEstados() {Estado = "Hidalgo", Capital = "Pachuca de Soto" });
            items.Add(new CiudadesEstados() {Estado = "Jalisco", Capital = "Guadalajara" });
            items.Add(new CiudadesEstados() {Estado = "México", Capital = "Toluca de Lerdo" });
            items.Add(new CiudadesEstados() {Estado = "Michoacán", Capital = "Morelia" });
            items.Add(new CiudadesEstados() {Estado = "Morelos", Capital = "Cuernavaca" });
            items.Add(new CiudadesEstados() {Estado = "Nayarit", Capital = "Tepic" });
            items.Add(new CiudadesEstados() {Estado = "Nuevo León", Capital = "Monterrey" });
            items.Add(new CiudadesEstados() {Estado = "Oaxaca", Capital = "Oaxaca de Juárez" });
            items.Add(new CiudadesEstados() {Estado = "Puebla", Capital = "Puebla de Zaragoza" });
            items.Add(new CiudadesEstados() {Estado = "Querétaro", Capital = "Santiago de Querétaro" });
            items.Add(new CiudadesEstados() {Estado = "Quintana Roo", Capital = "Chetumal" });
            items.Add(new CiudadesEstados() {Estado = "San Luis Potosí", Capital = "San Luis Potosí" });
            items.Add(new CiudadesEstados() {Estado = "Sinaloa", Capital = "Culiacán Rosales" });
            items.Add(new CiudadesEstados() {Estado = "Sonora", Capital = "Hermosillo" });
            items.Add(new CiudadesEstados() {Estado = "Tabasco", Capital = "Villahermosa" });
            items.Add(new CiudadesEstados() {Estado = "Tamaulipas", Capital = "Ciudad Victoria" });
            items.Add(new CiudadesEstados() {Estado = "Tlaxcala", Capital = "Tlaxcala de Xicohténcatl" });
            items.Add(new CiudadesEstados() {Estado = "Veracruz", Capital = "Xalapa" });
            items.Add(new CiudadesEstados() {Estado = "Yucatán", Capital = "Mérida" });
            items.Add(new CiudadesEstados() {Estado = "Zacatecas", Capital = "Zacatecas" });
            return items;
        }
    }
}