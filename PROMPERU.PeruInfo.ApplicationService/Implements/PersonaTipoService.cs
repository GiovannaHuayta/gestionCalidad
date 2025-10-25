using PROMPERU.PeruInfo.ApplicationService.Contracts;
using PROMPERU.PeruInfo.Domain.Contracts;
using PROMPERU.PeruInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROMPERU.PeruInfo.ApplicationService.Implements
{
    public class PersonaTipoService : IPersonaTipoService
    {
        #region Private Variables

        private readonly IPersonaTipoRepository _repository;

        #endregion

        #region Constructor

        public PersonaTipoService(IPersonaTipoRepository repository)
        {
            _repository = repository;
        }

        #endregion

        #region Public Methods

        public List<PersonaTipoBE> Listar(int idiomaId)
        {
            return _repository.Listar(idiomaId);
        }

        #endregion
    }
}
