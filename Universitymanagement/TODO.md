- Add Add(Department department) method to the interface.

## 2. Update Repositories/DepartmentRepository.cs
- Implement the Add method in DepartmentRepository.

## 3. Create Views/DepartmentView.cs
- Create new file with AddDepartment() method that prompts for department name and adds it.

## 4. Update Interfaces/IStudentRepository.cs
- Add Update(Student student) method to the interface.

## 5. Update Repositories/StudentRepository.cs
- Implement the Update method in StudentRepository.

## 6. Update Views/StudentView.cs
- Add UpdateStudentDepartment() method that prompts for student ID and department ID, validates, and updates.

## 7. Update Views/MainMenuView.cs
- Add DepartmentView to constructor injection.
- Add menu option "8. Add Department" calling _departmentView.AddDepartment().
- Add menu option "9. Assign Student to Department" calling _studentView.UpdateStudentDepartment().
- Update the menu display and switch case.

## 8. Test the Implementation
- Test adding a department.
- Test assigning a student to a department.
- Verify department report works.
=======
## 1. Update Interfaces/IDepartmentRepository.cs
- [x] Add Add(Department department) method to the interface.

## 2. Update Repositories/DepartmentRepository.cs
- [x] Implement the Add method in DepartmentRepository.

## 3. Create Views/DepartmentView.cs
- [x] Create new file with AddDepartment() method that prompts for department name and adds it.

## 4. Update Interfaces/IStudentRepository.cs
- [x] Add Update(Student student) method to the interface.

## 5. Update Repositories/StudentRepository.cs
- [x] Implement the Update method in StudentRepository.

## 6. Update Views/StudentView.cs
- [x] Add UpdateStudentDepartment() method that prompts for student ID and department ID, validates, and updates.

## 7. Update Views/MainMenuView.cs
- [x] Add DepartmentView to constructor injection.
- [x] Add menu option "8. Add Department" calling _departmentView.AddDepartment().
- [x] Add menu option "9. Assign Student to Department" calling _studentView.UpdateStudentDepartment().
- [x] Update the menu display and switch case.

## 8. Update Program.cs
- [x] Add DepartmentView to DI container.

## 9. Test the Implementation
- [ ] Run the application and test adding a department.
- [ ] Test assigning a student to a department.
- [ ] Verify department report works.
