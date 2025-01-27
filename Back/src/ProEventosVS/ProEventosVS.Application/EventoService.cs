﻿using ProEventosVS.Application.Contratos;
using ProEventosVS.Domain;
using ProEventosVS.Persistence.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventosVS.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersistence _geralPersistence;
        private readonly IEventoPersistence _eventoPersistence;
        public EventoService(IGeralPersistence geralPersistence, IEventoPersistence eventoPersistence)
        {
            _geralPersistence = geralPersistence;
            _eventoPersistence = eventoPersistence;
        }

        public async Task<Evento> AddEventos(Evento model)
        {
            try
            {
                _geralPersistence.Add(model);
                if (await _geralPersistence.SaveChangesAsync())
                {
                    return await _eventoPersistence.GetEventoByIdAsync(model.Id, false);
                }
                return null;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Evento> UpdateEventos(int eventoId, Evento model)
        {
            try
            {
                var evento = await _eventoPersistence.GetEventoByIdAsync(eventoId, false);
                if (evento == null)
                    return null;

                model.Id = evento.Id;

                _geralPersistence.Update(model);
                if (await _geralPersistence.SaveChangesAsync())
                {
                    return await _eventoPersistence.GetEventoByIdAsync(model.Id, false);
                }
                return null;

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteEventos(int eventoId)
        {
            try
            {
                var evento = await _eventoPersistence.GetEventoByIdAsync(eventoId, false);
                if (evento == null)
                    throw new Exception("O Evento não foi localizado para exclusão");

                _geralPersistence.Delete<Evento>(evento);
                return await _geralPersistence.SaveChangesAsync();             

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersistence.GetAllEventosAsync(includePalestrantes);
                if (eventos == null) return null;
                return eventos;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersistence.GetAllEventosByTemaAsync(tema, includePalestrantes);
                if (eventos == null) return null;
                return eventos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersistence.GetEventoByIdAsync(eventoId, includePalestrantes);
                if (eventos == null) return null;
                return eventos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        
    }
}
