import { PanelGroupModel } from './PanelGroupModel';
import { EmployeeModel } from './EmployeeModel';


export class EmployeePanelModel {
    public employeePanelMappingId: Number;
    public employeeId: Number;
    public panelGroupId: Number;
    public employee: EmployeeModel;
    public panelGroup: PanelGroupModel;
}