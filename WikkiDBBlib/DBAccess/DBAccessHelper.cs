using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WikkiDBBlib.DBAccess
{

    public class DBAccessHelper<Model> where Model : class
    {
        private AppDBContext _DbContext;
        private DbSet<Model> _DbSet;

        public DBAccessHelper()
        {
            _DbContext = new(); // AppDBContext();
            _DbSet = _DbContext.Set<Model>();
        }
        // -----------------------------------------------------------------------------
        // in includeModel können noch andere Tabellennamen stehen wo gesucht werden soll
        // Es kann auch zwei Tabellen enthalten z.B. "Stadt, Departement" 
        public IEnumerable<Model>? GetAll(Expression<Func<Model, bool>>? filter = null, string? includeModel = null)
        {
            try
            {
                // query erhält das komplette DBSet
                IQueryable<Model> _DBSetQuery = _DbSet;   
                if (filter != null)
                {
                    // Nachdem query das DbSet erhalten hat, kann jetzt der Filter
                    // über query angewendet werden
                    _DBSetQuery = _DBSetQuery.Where(filter);
                }

                if(includeModel != null)
                {
                    // Parameter includeModel untersuchen ob mehrere Tabellen drin stehen
                    var Models = includeModel.Split(",", StringSplitOptions.RemoveEmptyEntries);
                    foreach (var Model in Models)
                    {
                        _DBSetQuery = _DBSetQuery.Include(Model);
                    }
                }
                if (_DBSetQuery is not null)
                {
                        return _DBSetQuery.ToList();
                }
                return null;
                    
            }
            catch (Exception ex)
            {

                MessageBox.Show("GetByID GetAll (......)" + Environment.NewLine + ex.Message);
                return null;
            }
        }
        // -----------------------------------------------------------------------------
        public bool Update(Model iModel)
        {
            try
            {
                if (iModel == null) return false;

                _DbSet.Update(iModel);
                _DbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("GetByID Update" + Environment.NewLine + ex.Message);
                return true;
            }
        }
        // -----------------------------------------------------------------------------
        public bool DeletebyID(int iID)
        {
            try
            {
                if (iID <= 0) return false;

                var model = _DbSet.Find(iID);
                if (model == null) return false;
       
                _DbSet.Remove(model);
                _DbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("DeleteByID" + Environment.NewLine + ex.Message);
                return false;
            }
            
        }

        public bool PruefGetByID(Expression<Func<Model, bool>>? filter = null, string? includeModel = null)
        {
            // Nur Prüfen ob ein Datensatz gefunden wird mit der ID
            try
            {
                if (filter is null) return false;

               //  var model = _DbSet.Find(iID);
                var model = _DbSet.Where(filter);
                if (model == null) return false;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("GetByID" + Environment.NewLine + ex.Message);
                return false;
            }

        }
        // -----------------------------------------------------------------------------
        public bool Add(Model iModel)
        {
            try
            {
                if (iModel is null)
                {
                    return false;
                }
                _DbSet.Add(iModel);
                _DbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("GetByID" + Environment.NewLine + ex.Message);
                return false;
                throw;
            }
           
        }
        // -----------------------------------------------------------------------------
        public Model? GetByID(int iID)
        {
            Model? _Model = null;
            try
            {
                if (iID > 0)
                {
                    _Model = _DbSet?.Find(iID);
                }
                return _Model;
            }
            catch (Exception ex)
            {
                MessageBox.Show("GetByID:" + Environment.NewLine + ex.Message);    
                return _Model;  
            }

        }
    }
}
