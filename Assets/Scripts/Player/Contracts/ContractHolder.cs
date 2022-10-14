using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ContractHolder
{
    public List<Contract> contracts;
    private bool _hasMainContract = false;


    public ContractHolder()
    {
        contracts = new List<Contract>(4);
    }

    public void Add(Contract contract)
    {
        if (contract.type == Contract.contractType.Regular && (contracts.Count < 3 && !_hasMainContract)
            || (contracts.Count < 4 && _hasMainContract))
        {
            contracts.Add(contract);
            contract.StartContract();
        }
        else if(contract.type == Contract.contractType.Major && !_hasMainContract)
        {
            contracts.Add(contract);
            contract.StartContract();
            _hasMainContract = true;
        }
    }

    public bool IsFull()
    {
        if (contracts.Count == contracts.Capacity)
            return true;
        return false;
    }

    public void Remove(Contract contract)
    {
        if (contract.type == Contract.contractType.Major)
            _hasMainContract = false;
        contracts.Remove(contract);
    }

    public Contract FindContract(int ID)
    {
        foreach (var contract in contracts)
        {
            if (contract.ID == ID)
                return contract;
        }
        return null;
    }
}
