﻿using DocSeeker.API.Profiles.Domain.Models;
using DocSeeker.API.Profiles.Domain.Repositories;
using DocSeeker.API.Profiles.Domain.Services;
using DocSeeker.API.Profiles.Domain.Services.Communication;
using DocSeeker.API.Shared.Domain.Repositories;

namespace DocSeeker.API.Profiles.Services;

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DoctorService(IDoctorRepository doctorRepository, IUnitOfWork unitOfWork)
    {
        _doctorRepository = doctorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Doctor>> ListAsync()
    {
        return await _doctorRepository.ListAsync();
    }

    public async Task<DoctorResponse> SaveAsync(Doctor doctor)
    {
        try
        {
            await _doctorRepository.AddAsync(doctor);
            await _unitOfWork.CompleteAsync();
            
            return new DoctorResponse(doctor);
        }
        catch (Exception e)
        {
            return new DoctorResponse($"An error occurred when saving the doctor: {e.Message}");
        }
    }

    public async Task<DoctorResponse> UpdateAsync(int id, Doctor doctor)
    {
        var existingDoctor = await _doctorRepository.FindByIdAsync(id);
        
        if (existingDoctor == null)
            return new DoctorResponse("Doctor not found.");
        
        existingDoctor.Name = doctor.Name;
        existingDoctor.Lastname = doctor.Lastname;
        existingDoctor.Email = doctor.Email;
        existingDoctor.Phone = doctor.Phone;
        existingDoctor.Gender = doctor.Gender;
        existingDoctor.Middlename = doctor.Middlename;
        existingDoctor.Username = doctor.Username;
        existingDoctor.Speciality = doctor.Speciality;

        try
        {
            _doctorRepository.Update(existingDoctor);
            await _unitOfWork.CompleteAsync();
            
            return new DoctorResponse(existingDoctor); 
        }
        catch (Exception e)
        {
            return new DoctorResponse($"An error occurred when updating the doctor: {e.Message}");  
        }
    }

    public async Task<DoctorResponse> DeleteAsync(int id)
    {
        var existingDoctor = await _doctorRepository.FindByIdAsync(id);

        if (existingDoctor == null)
            return new DoctorResponse("Doctor not found.");

        try
        {
            _doctorRepository.Remove(existingDoctor);
            await _unitOfWork.CompleteAsync();

            return new DoctorResponse(existingDoctor);
        }
        catch (Exception e)
        {
            return new DoctorResponse($"An error occurred when deleting the doctor: {e.Message}");
        }
    }
}